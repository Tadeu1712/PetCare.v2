<template>
  <div class="modal fade" id="loginModal" role="dialog">
    <div class="modal-dialog modal-dialog-centered modal-login" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h4 class="modal-title">Sign In</h4>
          <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        </div>
        <div class="modal-body">
          <form class="login" @submit.prevent="login">
            <div class="form-group">
              <img src="/icon/email.svg" />
              <input v-model="email" type="email" class="form-control" placeholder="Email" required />
            </div>
            <div class="form-group">
              <img src="/icon/password.svg" />
              <input
                v-model="password"
                type="password"
                class="form-control"
                placeholder="Password"
                required
              />
            </div>
            <div class="form-group">
              <input
                @click="login()"
                type="submit"
                class="btn btn-primary btn-block btn-lg"
                value="Login"
              />
            </div>
          </form>
        </div>
        <div class="modal-footer">
          <a href="#">Forgot Password?</a>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import JQuery from "jquery";
let $ = JQuery;
const { url } = require("../../../helper");

export default {
  data() {
    return {
      email: null,
      password: null
    };
  },
  methods: {
    login: function() {
      const { email, password } = this;
      const vm = this;
      axios
        .post(url + "/auth/login", {
          email: email,
          password: password
        })
        .then(function(res) {
          const token = res.data.token;
          localStorage.setItem("token", token);
          $("#loginModal").modal("hide");
          if (res.data.data.admin) {
            vm.$router.push({
              path: "/dashboard/users",
              params: { feedback: "Great!" }
            });
          } else {
            vm.$router.push({
              name: "dashboard",
              params: { feedback: "Great!" }
            });
          }
        })
        .catch(function(e) {
          localStorage.removeItem("token");
          $("#loginModal").modal("hide");
          if (e.response) {
            vm.$router.push({
              name: "homepage",
              params: { feedback: e.response.data }
            });
          }
        });
    }
  }
};
</script>

<style scoped>
.modal-login {
  color: #636363;
  max-width: 400px;
}
.modal-login .modal-content {
  padding: 20px;
  border-radius: 5px;
  border: none;
}
.modal-login .modal-header {
  border-bottom: none;
  position: relative;
  justify-content: center;
}
.modal-login h4 {
  text-align: center;
  font-size: 26px;
}
.modal-login .form-group {
  position: relative;
}
.modal-login img {
  position: absolute;
  left: 13px;
  top: 11px;
  width: 18px;
}
.modal-login .form-control {
  padding-left: 40px;
}
.modal-login .form-control:focus {
  border-color: blue;
}
.modal-login .form-control,
.modal-login .btn {
  min-height: 40px;
  border-radius: 10px;
}
.modal-login .hint-text {
  text-align: center;
  padding-top: 10px;
}
.modal-login .close {
  position: absolute;
  top: -5px;
  right: -5px;
}
.modal-login .btn {
  background: blue;
  border: none;
  line-height: normal;
}
.modal-login .btn:hover,
.modal-login .btn:focus {
  background: blue;
}
.modal-login .modal-footer {
  background: #ecf0f1;
  border-color: #dee4e7;
  text-align: center;
  margin: 0 -20px -20px;
  font-size: 13px;
  justify-content: center;
  border-radius: 5px;
}
.modal-login .modal-footer a {
  color: #999;
}
</style>
