// JavaScript 解密工具
function decrypt(encryptedString) {
    // 检查输入是否为空
    if (!encryptedString || encryptedString.trim() === '') {
        return null;
    }

    // 去除最后一个 'A' 字符后的字符串
    let str1 = encryptedString.trim().substring(0, encryptedString.lastIndexOf('A'));
    let str2 = '';

    // 遍历字符串，处理特定字符
    for (let i = 0; i < str1.length; i++) {
        let currentChar = str1[i];
        let str3 = '';

        switch (currentChar) {
            case 'E':
                // 如果是 'E'，根据后续字符重复该字符
                let int16 = parseInt(str1[i + 1]);
                for (let j = 0; j < int16; j++) {
                    str3 += str1[i + 2];
                }
                i += 2;
                break;
            case 'D':
                // 如果是 'D'，重复后续字符四次
                str3 = str1[i + 1].repeat(4);
                i++;
                break;
            case 'C':
                // 如果是 'C'，重复后续字符三次
                str3 = str1[i + 1].repeat(3);
                i++;
                break;
            case 'B':
                // 如果是 'B'，重复后续字符两次
                str3 = str1[i + 1].repeat(2);
                i++;
                break;
            default:
                // 默认是直接使用该字符
                str3 = currentChar;
                break;
        }

        // 拼接处理后的字符
        str2 += str3;
    }

    // 转换成二进制
    let binaryString = '';
    for (let i = 0; i < str2.length; i++) {
        let hexValue = parseInt(str2[i], 16);
        binaryString += hexValue.toString(2).padStart(4, '0');
    }

    let rearrangedBinary = new Array(binaryString.length);
    let num1 = Math.floor(binaryString.length / 16);
    for (let i = 0; i < binaryString.length; i++) {
        let num3 = Math.floor(i / 16);
        let num4 = i % 16;
        let newIndex = num4 * num1 + num3;
        rearrangedBinary[newIndex] = newIndex % 2 === 1 ? (binaryString[i] === '1' ? '0' : '1') : binaryString[i];
    }

    let input = rearrangedBinary.join('');
    let sum = input.length.toString().split('').reduce((acc, num) => acc + parseInt(num), 0);

    // 进一步处理二进制数据
    for (let i = sum - 1; i < input.length; i += sum) {
        let num6 = input.slice(i - sum + 1, i).split('').reduce((acc, num) => acc + parseInt(num), 0);
        if (num6 % 2 === 1) {
            input = input.slice(0, i) + (input[i] === '0' ? '1' : '0') + input.slice(i + 1);
        }
    }

    // 移除所有 '1' 并计算数量
    let num7 = input.replace(/0/g, '').length;
    let length2 = parseInt(num7.toString().slice(-1), 10);

    if (length2 === 0) length2 = 7;
    if (length2 === 1) length2 = 13;

    for (let i = length2 - 1; i < input.length; i += length2) {
        let str14 = input.slice(i - (length2 - 1), i + 1);
        let reversedStr = [...str14].reverse().join('');
        input = input.slice(0, i - (length2 - 1)) + reversedStr + input.slice(i + 1);
    }

    // 提取二进制块，转为字符串
    let matches = input.match(/([01]{8})+/g);
    if (!matches) return '';

    let bytes = matches[0].match(/.{8}/g).map(byte => parseInt(byte, 2));
    let decodedString = String.fromCharCode(...bytes);

    return decodedString.substring(0, decodedString.lastIndexOf('A'));
}

// 测试调用函数
let encryptedString = "这里是加密的字符串";
let decryptedString = decrypt(encryptedString);
console.log("解密后的字符串: " + decryptedString);
