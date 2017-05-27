//replace multil text
/*---------------test-----------------
var index={
    "sang":"0",
    "suong":"1",
    "oanh":"2"
}
var text="sang suong shit oanh";

result=multiReplace(index, text);
//output: 0 1 shit 2
-------------------------------------*/
function multilReplace(index, text){
    var pattern = '';
    for (var i in index) {
        if (pattern != '') pattern += '|';
        pattern += i;
    }
    text.replace(new RegExp(pattern, 'g'), function($0) {
    return index[$0] != undefined ? index[$0] : $0;
    });
}
