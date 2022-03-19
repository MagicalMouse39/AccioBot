from socket import socket, AF_INET, SOCK_DGRAM
import cv2

sock = socket(AF_INET, SOCK_DGRAM)

def send_local(msg):
    sock.sendto(msg, ('127.0.0.1', 1337))

vcap = cv2.VideoCapture('video_1920.mp4')
while (vcap.isOpened()):
    res, frame = vcap.read()
    fbytes = cv2.imencode('.bmp', frame)[1].tobytes()
    print(f'Frame bytes length: {len(fbytes)}')
    n = 1024 * 50 # 50 KByte
    for chunk in [fbytes[x:x + n] for x in range(0, len(fbytes), n)]:
        print(f'Chunk length: {len(chunk)}')
        send_local(chunk)
    send_local(b'FRAME END')
    del fbytes

close(sock)
