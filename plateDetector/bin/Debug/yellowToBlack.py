import cv2
import numpy as np

# Baca gambar
image = cv2.imread('train_image/IMG-20240802-WA0009.jpg')

# Konversi gambar dari BGR ke HSV
hsv = cv2.cvtColor(image, cv2.COLOR_BGR2HSV)

# Definisikan rentang warna kuning dominan dalam HSV
lower_yellow = np.array([15, 100, 100])
upper_yellow = np.array([30, 255, 255])

# Buat mask untuk warna kuning dominan
mask_yellow = cv2.inRange(hsv, lower_yellow, upper_yellow)

# Lakukan operasi morfologi untuk memperbaiki mask
kernel = np.ones((5, 5), np.uint8)
mask_yellow = cv2.morphologyEx(mask_yellow, cv2.MORPH_CLOSE, kernel)
mask_yellow = cv2.morphologyEx(mask_yellow, cv2.MORPH_OPEN, kernel)

# Temukan kontur dari area yang dikenali
contours, _ = cv2.findContours(mask_yellow, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

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
        
        # Crop area plat nomor
        license_plate = image[y:y+h, x:x+w]

        # Upscale plat yang terdeteksi
        scale_factor = 2  # Faktor skala untuk upscale
        new_size = (license_plate.shape[1] * scale_factor, license_plate.shape[0] * scale_factor)
        upscaled_plate = cv2.resize(license_plate, new_size, interpolation=cv2.INTER_CUBIC)

        # 2. Lakukan Grayscale
        gray_plate = cv2.cvtColor(upscaled_plate, cv2.COLOR_BGR2GRAY)

        # 3. Invert gambar
        inverted_plate = cv2.bitwise_not(gray_plate)  

        # 4. Penyesuaian Kontras dan Kecerahan
        alpha = 2.0  # Meningkatkan kontras
        beta = -60   # Menyesuaikan kecerahan
        adjusted_plate = cv2.convertScaleAbs(inverted_plate, alpha=alpha, beta=beta)

        # 5. Tambahkan Gaussian Blur
        blurred_plate = cv2.GaussianBlur(adjusted_plate, (9, 9), 1.0) 
        
        # 6. Thresholding dengan nilai threshold yang lebih rendah
        _, thresholded_plate = cv2.threshold(blurred_plate, 200, 255, cv2.THRESH_BINARY)

        # 7. Menonjolkan warna putih dengan penyesuaian tambahan
        # Lakukan penyesuaian lebih lanjut untuk menonjolkan warna putih
        white_highlight = cv2.addWeighted(thresholded_plate, 1.5, np.zeros(thresholded_plate.shape, dtype=np.uint8), 0, 0)

        # Tandai area plat nomor pada gambar asli
        cv2.rectangle(result_image, (x, y), (x+w, y+h), (0, 255, 0), 2)
        
        # Tampilkan gambar hasil
        cv2.imshow('Grayscale Plate', cv2.resize(gray_plate, (0, 0), fx=0.5, fy=0.5))
        cv2.imshow('Inverted Plate', cv2.resize(inverted_plate, (0, 0), fx=0.5, fy=0.5))
        cv2.imshow('Adjusted Plate', cv2.resize(adjusted_plate, (0, 0), fx=0.5, fy=0.5))
        cv2.imshow('Blurred Plate', cv2.resize(blurred_plate, (0, 0), fx=0.5, fy=0.5))
        cv2.imshow('Thresholded Plate', cv2.resize(thresholded_plate, (0, 0), fx=0.5, fy=0.5))
        cv2.imshow('White Highlighted Plate', cv2.resize(white_highlight, (0, 0), fx=0.5, fy=0.5))
    else:
        print("Tidak ada kontur plat nomor yang terdeteksi.")
        # Tampilkan gambar mask jika tidak ada kontur
        cv2.imshow('Mask Yellow', cv2.resize(mask_yellow, (0, 0), fx=0.5, fy=0.5))

# Tampilkan gambar asli dengan kotak hijau jika ada area plat nomor yang ditandai
cv2.imshow('Original Image with Plate Detection', cv2.resize(result_image, (0, 0), fx=0.5, fy=0.5))
cv2.waitKey(0)
cv2.destroyAllWindows()

# Simpan gambar hasil
if contours:
    cv2.imwrite('D:\\Img\\image_hasil.jpg', white_highlight)
else:
    cv2.imwrite('D:\\Img\\image_hasil.jpg', mask_yellow)
