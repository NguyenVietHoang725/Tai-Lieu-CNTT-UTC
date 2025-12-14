const username = document.getElementById("username");
const password = document.getElementsByName("password");

function login() {
  if (username.value.trim === "" || password[0].value === "") {
    alert("Vui lòng nhập tên đăng nhập và mật khẩu!");
  }
  if (!validateEmail(username.value)) {
    alert("Vui lòng nhập địa chỉ Email hợp lệ!");
    return;
  }

  if (!validatePassword(password[0].value)) {
    alert("Mật khẩu phải có ít nhất 6 ký tự!");
    return;
  }

  alert("Đăng nhập thành công!");
}

function validateEmail(email) {
  const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return regex.test(email);
}

function validatePassword(password) {
  return password.length >= 6;
}
