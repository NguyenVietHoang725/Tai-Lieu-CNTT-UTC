// coding implementation
document.getElementById("submit").addEventListener("click", function (event) {
  event.preventDefault();
  let isValid = true;

  const keyword = document.getElementById("key").value.trim();
  const category = document.getElementById("cat").value.trim();
  const priceFrom = document.getElementById("from").value.trim();
  const priceTo = document.getElementById("to").value.trim();

  if (keyword === "") {
    alert("Vui lòng nhập từ khóa.");
    isValid = false;
  }
  if (category === "") {
    alert("Vui lòng chọn danh mục.");
    isValid = false;
  }

  if (priceFrom === "" || priceTo === "") {
    alert("Vui lòng điền giá tiền");
    isValid = false;
  }
  if (priceFrom !== "" && isNaN(priceFrom)) {
    alert("Giá từ phải là một số.");
    isValid = false;
  }
  if (priceTo !== "" && isNaN(priceTo)) {
    alert("Giá đến phải là một số.");
    isValid = false;
  }
  if (priceFrom < 0 || priceTo < priceFrom) {
    alert("Giá vừa nhập không hợp lệ.");
    isValid = false;
  }

  if (isValid) {
    alert("Tìm kiếm thành công!");
    document.querySelector("form").submit();
  }
});
// end coding implementation
