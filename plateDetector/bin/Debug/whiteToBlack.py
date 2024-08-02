import cv2
import numpy as np

# Baca gambar
image = cv2.imread('train_image/IMG-20240802-WA0004.jpg')

# Konversi gambar dari BGR ke HSV
hsv = cv2.cvtColor(image, cv2.COLOR_BGR2HSV)

# Definisikan rentang warna putih dalam HSV
lower_white = np.array([0, 0, 180])  # Rentang warna putih (terang)
upper_white = np.array([180, 50, 255])  # Rentang warna putih (terang)

# Buat mask untuk warna putih
mask_white = cv2.inRange(hsv, lower_white, upper_white)

# Lakukan operasi morfologi untuk memperbaiki mask
kernel = np.ones((9, 9), np.uint8)  # Ukuran kernel morfologi yang lebih besar
mask_white = cv2.morphologyEx(mask_white, cv2.MORPH_CLOSE, kernel)
mask_white = cv2.morphologyEx(mask_white, cv2.MORPH_OPEN, kernel)

# Temukan kontur dari area yang dikenali
contours, _ = cv2.findContours(mask_white, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

# Menyiapkan gambar hasil untuk ditampilkan jika tidak ada kontur terdeteksi
result_image = image.copy()

# Filter dan ambil kontur dengan area terbesar sebagai plat nomor
if contours:
    # Menyaring kontur berdasarkan area dan rasio aspek
    filtered_contours = []
    for c in contours:
        x, y, w, h = cv2.boundingRect(c)
        aspect_ratio = float(w) / h
        if 2 < aspect_ratio < 6:  # Misalnya, rasio aspek untuk plat nomor
            filtered_contours.append(c)
    
    if filtered_contours:
        c = max(filtered_contours, key=cv2.contourArea)
        x, y, w, h = cv2.boundingRect(c)
        
        # Tambahkan padding pada area plat nomor
        padding = 20  # Ukuran padding
        x -= padding
        y -= padding
        w += 2 * padding
        h += 2 * padding
        
        # Pastikan nilai padding tidak melampaui batas gambar
        x = max(x, 0)
        y = max(y, 0)
        w = min(w, image.shape[1] - x)
        h = min(h, image.shape[0] - y)
        
        # Crop area plat nomor
        license_plate = image[y:y+h, x:x+w]

        # Upscale plat yang terdeteksi
        scale_factor = 1.5  # Faktor skala untuk upscale
        new_size = (int(license_plate.shape[1] * scale_factor), int(license_plate.shape[0] * scale_factor))
        upscaled_plate = cv2.resize(license_plate, new_size, interpolation=cv2.INTER_CUBIC)

        # Konversi gambar berwarna ke grayscale untuk thresholding
        gray_plate = cv2.cvtColor(upscaled_plate, cv2.COLOR_BGR2GRAY)

        # Lakukan Thresholding langsung pada gambar berwarna
        _, thresholded_plate = cv2.threshold(gray_plate, 215, 255, cv2.THRESH_BINARY)

        # 1. Invert gambar untuk mendapatkan teks hitam di atas latar belakang putih
        inverted_plate = cv2.bitwise_not(thresholded_plate)

        # 2. Tambahkan Gaussian Blur untuk menghaluskan gambar
        blurred_plate = cv2.GaussianBlur(inverted_plate, (7, 7), 0)  # Ukuran kernel yang lebih besar
        
        # Tandai area plat nomor pada gambar asli
        cv2.rectangle(result_image, (x, y), (x+w, y+h), (0, 255, 0), 2)
        
        # Tampilkan gambar hasil
        cv2.imshow('Thresholded Plate', cv2.resize(thresholded_plate, (0, 0), fx=0.5, fy=0.5))
        cv2.imshow('Inverted Plate', cv2.resize(inverted_plate, (0, 0), fx=0.5, fy=0.5))
        cv2.imshow('Blurred Plate', cv2.resize(blurred_plate, (0, 0), fx=0.5, fy=0.5))
    else:
        print("Tidak ada kontur plat nomor yang terdeteksi.")
        # Tampilkan gambar mask jika tidak ada kontur
        cv2.imshow('Mask White', cv2.resize(mask_white, (0, 0), fx=0.5, fy=0.5))

# Tampilkan gambar asli dengan kotak hijau jika ada area plat nomor yang ditandai
cv2.imshow('Original Image with Plate Detection', cv2.resize(result_image, (0, 0), fx=0.5, fy=0.5))
cv2.waitKey(0)
cv2.destroyAllWindows()

# Simpan gambar hasil
if contours:
    cv2.imwrite('D:\\Img\\image_hasil.jpg', blurred_plate)
else:
    cv2.imwrite('D:\\Img\\image_hasil.jpg', mask_white)
