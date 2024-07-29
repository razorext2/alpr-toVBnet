import os
import cv2
import imutils
import numpy as np

import Calibration as cal
import DetectChars
import DetectPlates
import Preprocess as pp

# Module level variables for image
SCALAR_BLACK = (0, 0, 0)
SCALAR_WHITE = (255, 255, 255)
SCALAR_YELLOW = (0, 255, 255)
SCALAR_GREEN = (0, 255, 0)
SCALAR_RED = (0, 0, 255)
N_VERIFY = 5  # number of verifications

def main():
    """Main function to process the image and detect license plates."""
    image_path = 'D:/102.jpg'

    img_original_scene = cv2.imread(image_path)
    if img_original_scene is None:
        print("Error: Please check the image path or argument!")
        return

    if not DetectChars.loadKNNDataAndTrainKNN():
        print("Error: KNN training failed!")
        return

    img_original_scene = imutils.resize(img_original_scene, width=720)
    cv2.imshow("Original", img_original_scene)

    img_grayscale, img_thresh = pp.preprocess(img_original_scene)
    cv2.imshow("Threshold", img_thresh)

    img_original_scene, new_license = search_license_plate(img_original_scene, False)
    print(f"License plate read from image: {new_license}\n")

    cv2.waitKey(0)
    cv2.destroyAllWindows()

def draw_red_rectangle_around_plate(img, lic_plate):
    """Draw a red rectangle around the detected license plate."""
    rect_points = cv2.boxPoints(lic_plate.rrLocationOfPlateInScene)
    
    # Convert points to integer
    rect_points = np.int0(rect_points)
    
    for i in range(4):
        pt1, pt2 = tuple(rect_points[i]), tuple(rect_points[(i + 1) % 4])
        cv2.line(img, pt1, pt2, SCALAR_RED, 2)

def write_license_plate_chars_on_image(img, lic_plate):
    """Write the detected license plate characters on the image."""
    scene_height, scene_width, _ = img.shape
    plate_height, plate_width, _ = lic_plate.imgPlate.shape

    font_face = cv2.FONT_HERSHEY_SIMPLEX
    font_scale = float(plate_height) / 30.0
    font_thickness = int(round(font_scale * 1.5))

    text_size, baseline = cv2.getTextSize(lic_plate.strChars, font_face, font_scale, font_thickness)
    (plate_center_x, plate_center_y), (plate_width, plate_height), _ = lic_plate.rrLocationOfPlateInScene

    text_center_x = int(plate_center_x)
    if plate_center_y < scene_height * 0.75:
        text_center_y = int(round(plate_center_y)) + int(round(plate_height * 1.6))
    else:
        text_center_y = int(round(plate_center_y)) - int(round(plate_height * 1.6))

    text_origin_x = int(text_center_x - (text_size[0] / 2))
    text_origin_y = int(text_center_y + (text_size[1] / 2))

    cv2.putText(img, lic_plate.strChars, (text_origin_x, text_origin_y), font_face, font_scale, SCALAR_YELLOW, font_thickness)

def search_license_plate(img, loop):
    """Detect license plates and characters in the image."""
    licenses = ""
    if img is None:
        print("Error: Image not read from file")
        return img, licenses

    possible_plates = DetectPlates.detectPlatesInScene(img)
    possible_plates = DetectChars.detectCharsInPlates(possible_plates)

    if not loop:
        cv2.imshow("Original Scene", img)

    if not possible_plates:
        if not loop:
            print("No license plates were detected\n")
    else:
        possible_plates.sort(key=lambda plate: len(plate.strChars), reverse=True)
        lic_plate = possible_plates[0]

        if not loop:
            cv2.imshow("Plate", lic_plate.imgPlate)
            cv2.imshow("Threshold", lic_plate.imgThresh)

        if not lic_plate.strChars:
            if not loop:
                print("No characters were detected\n")
            return img, licenses

        draw_red_rectangle_around_plate(img, lic_plate)
        write_license_plate_chars_on_image(img, lic_plate)
        licenses = lic_plate.strChars

        if not loop:
            # print(f"License plate read from image: {lic_plate.strChars}\n")
            cv2.imshow("Original Scene", img)
            cv2.imwrite("imgOriginalScene.png", img)

    return img, licenses

if __name__ == "__main__":
    main()
