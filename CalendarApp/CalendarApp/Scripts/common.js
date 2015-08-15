function parseDate(str) {
    var s = str.split(" "),
        d = str[0].split("-"),
        t = str[1].replace(/:/g, "");
    return d[2] + d[1] + d[0] + t;
}