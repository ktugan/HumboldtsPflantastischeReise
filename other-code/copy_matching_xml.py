__author__ = 'johannes'
from bs4 import BeautifulSoup
import os
import shutil

#simple script for finding xml with with desired property and copying it in subfolder
class copy_matching_xml(object):

    def __init__(self, folderpath, subfoldername, nodename, searchterm, verbose=False, overwrite=False):
        self.folderpath = folderpath
        self.subfolderpath = folderpath + "/" + subfoldername
        self.nodename = nodename
        self.searchterm = searchterm
        self.parsedfiles = 0
        self.nodecount = 0
        self.searchresults = 0
        self.filenumber = str(len(os.listdir("/" + folderpath)))
        self.verbose = verbose

        if os.path.exists(self.subfolderpath):
            if overwrite:
                shutil.rmtree(self.subfolderpath, ignore_errors=True)
                os.mkdir(self.subfolderpath)
        else :
            os.mkdir(self.subfolderpath)
        for filename in os.listdir("/" + folderpath):
            self.searchXML(filename)
        self.stats()

    def searchXML(self, filename):
        if not filename.endswith(".xml"):
            return
        abspath = self.folderpath + "/" + filename
        file = open(abspath)
        soup = BeautifulSoup(file, "xml")
        result = soup.find(self.nodename)
        if result != None:
            self.nodecount += 1
            if self.searchterm in str(result):
                shutil.copy(abspath, self.subfolderpath)
                self.searchresults += 1
        file.close()
        self.parsedfiles += 1
        if self.verbose:
            print("Parsed " + str(self.parsedfiles) + " of " + self.filenumber + " files (" + str(self.searchresults) + " results)")

    def stats(self):
        print("Found " + str(self.nodecount) + " nodes with name: " + self.nodename)
        print("Found " + str(self.searchresults) + " results for the term: " + self.searchterm)

# call with own folderpath, subfoldername, nodename to be checked and searchterm
searchfolder = copy_matching_xml("/Path/2/Folder/", "Humboldt", "Collector", 'Humboldt', True, True)