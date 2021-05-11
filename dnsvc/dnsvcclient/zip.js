const fs = require('fs');
const path = require('path');
const JSZip = require('jszip');

let zip = new JSZip();
let root = path.resolve(__dirname, 'dist');

function zipFolder(...folder) {
    let r = path.resolve(root, ...folder);
    let z = zip;
    folder.forEach(i => z = z.folder(i));
    for (let n of fs.readdirSync(r)) {
        let p = path.resolve(r, n);
        let s = fs.statSync(p);
        if (s.isDirectory()) {
            zipFolder(...folder, n);
        } else {
            z.file(n, fs.readFileSync(p));
        }
    }
}

for (let n of fs.readdirSync(root)) {
    let p = path.resolve(root, n);
    let s = fs.statSync(p);
    if (s.isDirectory()) {
        zipFolder(n);
    }
    if (s.isFile()) {
        zip.file(n, fs.readFileSync(p));
    }
}

zip.generateNodeStream({streamFiles:true})
.pipe(fs.createWriteStream('www.zip'));