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


function hilight1 (){
  let ratingField = document.querySelector(".rating")
  ratingField.value = 1
}
starsS.addEventListener("click", hilight1)
  
function hilight2(){
  let ratingField2 = document.querySelector(".rating")
  ratingField2.value = 2
}
starsT.addEventListener("click",hilight2)

function hilight3(){
  let ratingField3 = document.querySelector(".rating")
  ratingField3.value = 3
}
starsA.addEventListener("click",hilight3)

function hilight4(){
  let ratingField4 = document.querySelector(".rating")
  ratingField4.value = 4
}
starsR.addEventListener("click",hilight4)

function hilight5(){
  let ratingField5 = document.querySelector(".rating")
  ratingField5.value = 5
}
starsZ.addEventListener("click",hilight5)



