// Hàm để cập nhật số ngày trong tháng
function updateDays() {
  const month = document.getElementById("month").value;
  const daySelect = document.getElementById("day");
  const yearSelect = document.getElementById("year");

  daySelect.innerHTML =
    '<option value="" disabled selected hidden>Chọn ngày</option>';

  let daysInMonth;
  const year = yearSelect.value;

  // Đếm số ngày trong tháng
  if (month === "2") {
    // Kiểm tra năm nhuận
    daysInMonth =
      year && year % 4 === 0 && (year % 100 !== 0 || year % 400 === 0)
        ? 29
        : 28;
  } else if (["4", "6", "9", "11"].includes(month)) {
    daysInMonth = 30;
  } else {
    daysInMonth = 31;
  }

  // Thêm các tùy chọn ngày
  for (let i = 1; i <= daysInMonth; i++) {
    const option = document.createElement("option");
    option.value = i;
    option.textContent = i;
    daySelect.appendChild(option);
  }
}

// Tạo danh sách năm từ 1900 đến năm hiện tại
function populateYearSelect() {
  const yearSelect = document.getElementById("year");
  const currentYear = new Date().getFullYear();

  for (let i = 1900; i <= currentYear; i++) {
    const option = document.createElement("option");
    option.value = i;
    option.textContent = i;
    yearSelect.appendChild(option);
  }
}

// Hàm kiểm tra định dạng email và xác nhận email
function validateEmail() {
  const emailInput = document.getElementById("email");
  const confirmEmailInput = document.getElementById("confirmEmail");
  const emailHint = document.getElementById("emailHint");

  // Kiểm tra định dạng email
  const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  const emailIsValid = emailPattern.test(emailInput.value);

  if (!emailIsValid) {
    emailHint.textContent = "Email không hợp lệ.";
    emailHint.style.color = "red";
  } else {
    emailHint.textContent = ""; // Xóa thông báo lỗi nếu email hợp lệ
  }

  // Kiểm tra xác nhận email
  if (confirmEmailInput.value !== emailInput.value) {
    emailHint.textContent = "Email xác nhận không khớp.";
    emailHint.style.color = "red";
  } else if (emailIsValid) {
    emailHint.textContent = ""; // Xóa thông báo lỗi nếu email xác nhận hợp lệ
  }
}

// Hàm kiểm tra và hiển thị lỗi cho số điện thoại
function validatePhone() {
  const phoneInput = document.getElementById("phone");
  const phoneError = document.getElementById("phoneError");

  // Biểu thức regex để kiểm tra số điện thoại Việt Nam
  const phoneRegex = /^(\+84|0)(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-9]|9[0-9])\d{7}$/;

  if (!phoneRegex.test(phoneInput.value)) {
    phoneError.textContent = "Số điện thoại không hợp lệ. Vui lòng nhập lại.";
    phoneError.style.color = "red";
    return false;
  } else {
    phoneError.textContent = "";
    return true;
  }
}

// Hàm kiểm tra tất cả các trường đã được điền
function validateForm() {
  const requiredFields = [
    "firstName",
    "lastName",
    "email",
    "confirmEmail",
    "gender",
    "month",
    "day",
    "year",
    "country",
    "address",
    "city",
    "district",
    "zipcode",
    "phone",
  ];

  for (const field of requiredFields) {
    const inputElement = document.getElementById(field);
    if (inputElement && !inputElement.value) {
      alert(
        `Vui lòng điền vào trường: ${inputElement.previousElementSibling.textContent}`
      );
      return false;
    }
  }
  return true;
}

document
  .getElementById("personalInfoForm")
  .addEventListener("submit", function (event) {
    if (!validateForm() || !validatePhone()) {
      event.preventDefault();
    } else {
      validateEmail();
    }
  });

populateYearSelect();
