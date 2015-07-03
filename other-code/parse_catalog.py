__author__ = 'kadir'
import re

pattern = r'rdf:about="(.*?)"'
p = re.compile(pattern)

with open('links.txt', 'w') as output:
    with open('catalog.xml', encoding='utf8') as f:
        for line in f:
            matches = p.findall(line)
            for m in matches:
                output.write(m + '\n')