const selfsigned = require('selfsigned');
const fs = require('fs');
const path = require('path');

const attrs = [{ name: 'commonName', value: 'localhost' }];
const options = { keySize: 2048, days: 365 };

const pems = selfsigned.generate(attrs, options);

const certsDir = path.join(__dirname, 'certs');
if (!fs.existsSync(certsDir)) {
    fs.mkdirSync(certsDir);
}

fs.writeFileSync(path.join(certsDir, 'react.crt'), pems.cert);
fs.writeFileSync(path.join(certsDir, 'react.key'), pems.private);

console.log('Certificate and key generated in ./certs/');
