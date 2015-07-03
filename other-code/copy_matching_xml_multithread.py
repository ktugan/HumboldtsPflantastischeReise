__author__ = 'johannes'
from bs4 import BeautifulSoup
import os
import multiprocessing
import shutil
import re

#simple script for finding xml with with desired property and copying it in subfolder
overwrite = True
folderpath = "/Users/johannes/Desktop/Humboldt"
subfoldername = "PreciseCoordinates"
subfolderpath = folderpath + "/" + subfoldername
#can be string or regex
pattern = "[a*]"
searchterm = re.compile(pattern)
searchterm = "Ecuador"
nodename = "Locality"


def searchXML(filename):
    if not filename.endswith(".xml"):
        return
    abspath = folderpath + "/" + filename
    file = open(abspath)
    soup = BeautifulSoup(file, "xml")
    result = soup.find(nodename)
    if result != None:
        if type(searchterm) is str:
            if searchterm in str(result):
                shutil.copy(abspath, subfolderpath)
        else:
            if re.search(searchterm,str(result)):
                shutil.copy(abspath, subfolderpath)
    file.close()

# main
if os.path.exists(subfolderpath):
    if overwrite:
        shutil.rmtree(subfolderpath, ignore_errors=True)
        os.mkdir(subfolderpath)
else :
    os.mkdir(subfolderpath)

pool = multiprocessing.Pool()
pool.map(searchXML, os.listdir("/" + folderpath))
print("Found " + str(len(os.listdir(subfolderpath))) + " results")
