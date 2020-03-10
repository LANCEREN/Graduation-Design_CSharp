import cv2
import argparse

ap = argparse.ArgumentParser(description="test")
ap.add_argument("-i","--input",type=str,help="input path")
arg = ap.parse_args()

path = arg.input
img = cv2.imread(path)
cv2.namedWindow("win",cv2.WINDOW_AUTOSIZE)
cv2.imshow("win",img)

cv2.waitKey(500)
cv2.destroyAllWindows()
print("----")
