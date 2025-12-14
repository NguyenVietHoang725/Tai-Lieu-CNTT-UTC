const images = document.getElementsByClassName("img");

let index = 0;

function showImage(index) {
  for (let i = 0; i < images.length; i++) {
    if (!images[i].classList.contains("hidden")) {
      images[i].classList.add("hidden");
    }
  }

  images[index].classList.remove("hidden");
}

function next() {
  index++;
  if (index >= images.length) {
    index = 0;
  }
  showImage(index);
}

function prev() {
  index--;
  if (index < 0) {
    index = images.length - 1;
  }
  showImage(index);
}
