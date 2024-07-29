import socket #memuat modul/library socket untuk koneksi ke vb.net
import random #memuat modul/library untuk data angka random
import time #memuat modul/library untuk jeda pengiriman data

# Membuat objek socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Mengikat socket ke host publik dan port tertentu yang sama dengan vb.net
server_address = ('localhost', 9000)
sock.bind(server_address)

# Mendengarkan koneksi masuk
sock.listen(1)

# Menunggu koneksi masuk
print('Menunggu koneksi masuk...')
conn, client_address = sock.accept()
print('Koneksi dari', client_address)

# Loop untuk mengirim data dalam stream3
i=1
while True: # Membuat fungsi looping secara terus-menerus
    # Memperbarui data variabel di sini
    
    # Mengirim data angka random
    data1 = "{:.2f}".format(random.uniform(1, 100))
    # Mengirim data waktu random
    data2 = str(time.time())
    # Mengirim data angka secara urut
    i += 1
    data3 = str(i)
    # Menampilkan hasil yang dikirim pada cmd python
    print('Mengirim:', data1, data2, data3)

    # Mengirim data ke vb.net
    conn.sendall(bytes(data1 + ',' + data2 + ',' + data3, 'utf-8'))

    # Jeda waktu pengiriman data
    time.sleep(1.0)