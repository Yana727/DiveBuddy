// Write your JavaScript code.
/* When the user clicks on the button, 
toggle between hiding and showing the dropdown content */
function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}

// Close the dropdown menu if the user clicks outside of it
window.onclick = function(event) {
  if (!event.target.matches('.dropbtn')) {

    var dropdowns = document.getElementsByClassName("dropdown-content");
    var i;
    for (i = 0; i < dropdowns.length; i++) {
      var openDropdown = dropdowns[i];
      if (openDropdown.classList.contains('show')) {
        openDropdown.classList.remove('show');
      }
    }
  }
}


let starsS = document.querySelector('.star-1')
let starsT = document.querySelector('.star-2')
let starsA = document.querySelector('.star-3')
let starsR = document.querySelector('.star-4')
let starsZ = document.querySelector('.star-5')


function hilight (){
  console.log ("star1")
}
starsS.addEventListener("click", hilight)



