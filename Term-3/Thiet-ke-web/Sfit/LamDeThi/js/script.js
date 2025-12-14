document.getElementById("myForm").addEventListener("submit", function (event) {
  event.preventDefault();

  let isValid = true;

  // Kiểm tra trường Name
  const nameInput = document.getElementById("name");
  const nameError = document.getElementById("name-error");
  const namePattern = /^[A-Za-zÀ-ỹ\s]+$/;

  if (!namePattern.test(nameInput.value)) {
    nameError.textContent = "Name chỉ chứa chữ cái và dấu cách.";
    isValid = false;
  } else {
    nameError.textContent = "";
  }

  // Kiểm tra trường Email
  const emailInput = document.getElementById("email");
  const emailError = document.getElementById("email-error");
  const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

  if (!emailPattern.test(emailInput.value)) {
    emailError.textContent = "Email không đúng định dạng.";
    isValid = false;
  } else {
    emailError.textContent = "";
  }

  // Kiểm tra trường Phone Number
  const phoneInput = document.getElementById("phone");
  const phoneError = document.getElementById("phone-error");
  const phonePattern =
    /^(?:\+?84|0)?(?:\s*\(\d{3}\)\s*|\d{3})(?:[\s.-]?\d{3}){2}$/;

  if (!phonePattern.test(phoneInput.value)) {
    phoneError.textContent = "Số điện thoại không đúng định dạng.";
    isValid = false;
  } else {
    phoneError.textContent = "";
  }

  if (isValid) {
    alert("Form đã submit thành công!");
  }
});
