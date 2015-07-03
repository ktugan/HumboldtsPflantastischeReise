__author__ = 'kadir'
from concurrent.futures import ThreadPoolExecutor
import re
from time import sleep
from urllib.request import urlretrieve
import sys
import os

rdf_url = 'http://herbarium.bgbm.org/data/rdf/'
pattern = r'http://herbarium.bgbm.org/object/(.*)'
dl_path = '''rdfs/{}.xml'''
p = re.compile(pattern)


def dl(dl_url, m):
    path = dl_path.format(m)
    if os.path.exists(path):
        return
    retry = 0
    while retry < 10:
        try:
            urlretrieve(dl_url, path)
            print(m)
            return
        except Exception as exc:
            exception = exc
        retry += 1
        sleep(retry)
    print(m, exception)


with ThreadPoolExecutor(max_workers=50) as e:
    with open('links.txt') as f:
        for line in f:
            m = p.findall(line)[0]
            url = rdf_url + m
            e.submit(dl, url, m)