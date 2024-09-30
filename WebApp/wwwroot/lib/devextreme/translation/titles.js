
var countTitle = $(".titleMain").length;
var countTitleSub = $(".titleSub").length;

for (var i = 0; i < countTitle; i++) {
    var title = $(".titleMain")[i].textContent;
    $(".titleMain")[i].innerHTML = "<center><h2>" + title + "<h2><hr /></center>";
}

for (var i = 0; i < countTitleSub; i++) {
    var titleSub = $(".titleSub")[i].textContent;
    $(".titleSub")[i].innerHTML = "<center><br /><hr /><h2>" + titleSub + "<h2></center>";
}