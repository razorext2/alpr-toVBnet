import cv2
import numpy as np

# Baca gambar
image = cv2.imread('D:\\Img\\kuning.jpg')

# Konversi gambar dari BGR ke HSV
hsv = cv2.cvtColor(image, cv2.COLOR_BGR2HSV)

# Definisikan rentang warna kuning dalam HSV
lower_yellow = np.array([20, 100, 100])
upper_yellow = np.array([30, 255, 255])

# Buat mask untuk warna kuning
mask_yellow = cv2.inRange(hsv, lower_yellow, upper_yellow)

# Temukan kontur dari area kuning
contours, _ = cv2.findContours(mask_yellow, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

# Ambil kontur dengan area terbesar sebagai plat nomor
if contours:
    c = max(contours, key=cv2.contourArea)
    x, y, w, h = cv2.boundingRect(c)
    
    # Crop area plat nomor
    license_plate = image[y:y+h, x:x+w]

    # Upscale plat yang terdeteksi
    scale_factor = 1  # Faktor skala untuk upscale
    new_size = (license_plate.shape[1] * scale_factor, license_plate.shape[0] * scale_factor)
    upscaled_plate = cv2.resize(license_plate, new_size, interpolation=cv2.INTER_CUBIC)

    # 2. Lakukan Grayscale
    gray_plate = cv2.cvtColor(upscaled_plate, cv2.COLOR_BGR2GRAY)

    # 3. Invert gambar
    inverted_plate = cv2.bitwise_not(gray_plate)  

    # 4. Penyesuaian Kontras dan Kecerahan
    alpha = 1.5  # Kurangi kontras
    beta = -50    # Sesuaikan kecerahan
    adjusted_plate = cv2.convertScaleAbs(inverted_plate, alpha=alpha, beta=beta)

    # 5. Tambahkan Gaussian Blur
    blurred_plate = cv2.GaussianBlur(adjusted_plate, (5, 5), 0) 
    
    # 6. Thresholding
    _, thresholded_plate = cv2.threshold(blurred_plate, 200, 255, cv2.THRESH_BINARY)

    # Tampilkan gambar hasil
    cv2.imshow('Grayscale Plate', gray_plate)
    cv2.imshow('Inverted Plate', inverted_plate)
    cv2.imshow('Adjusted Plate', adjusted_plate)
    cv2.imshow('Thresholded Plate', thresholded_plate)
    cv2.waitKey(0)
    cv2.destroyAllWindows()

    # Simpan gambar hasil
    cv2.imwrite('D:\\Img\\image_hasil.jpg', thresholded_plate)
else:
    print("Tidak ada area kuning yang terdeteksi.")
