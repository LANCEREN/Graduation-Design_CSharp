import cv2
import argparse

ap = argparse.ArgumentParser(description="python part")
input = ap.add_mutually_exclusive_group()
input.add_argument("-f","--file",type=str,help="input file path",default="default")
input.add_argument("--folder",type=str,help="input folder path",default="default")
arg = ap.parse_args()

filePath = arg.file
folderPath = arg.folder

if filePath != "default":
	print(filePath)
	#img = cv2.imread(filePath)
	#cv2.namedWindow("win",cv2.WINDOW_AUTOSIZE)
	#cv2.imshow("win",img)

	#cv2.waitKey(500)
	#cv2.destroyAllWindows()
	print("----")
elif folderPath != "default":
	pass
else:
	print("No Input !")
