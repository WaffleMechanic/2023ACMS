// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function HideShowArtwork() {

    var artwork02title = document.getElementById("Artwork02title");
    var artwork02filename = document.getElementById("Artwork02filename");
    var artwork02media = document.getElementById("Artwork02media");

    var artwork03title = document.getElementById("Artwork03title");
    var artwork03filename = document.getElementById("Artwork03filename");
    var artwork03media = document.getElementById("Artwork03media");

    // Controls Artwork02
    if (artwork02title.style.display == "none") {

        artwork02title.style.display = "block";
        artwork02filename.style.display = "block";
        artwork02media.style.display = "block";

    }
    else if (artwork02title.style.display == "block") {

        artwork03title.style.display = "block";
        artwork03filename.style.display = "block";
        artwork03media.style.display = "block";

    }
    else {
        window.print(); //Change this.
    }
} 

function labelCounter() {
    window.print();
    var i = 0;

    while (i < 3) {
        i++
        window.print();
        if (i == 3) {
            i = 0
            
            document.write("<br>");
        }
    }
}

