import Vue from 'vue'
import VueRouter from 'vue-router';
import App from "./App.vue";
import routes from './routes'
import axios from "axios";
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
const { url } = require("../helper");

Vue.config.productionTip = false

Vue.use(VueRouter);
const router = new VueRouter({
  scrollBehavior(to, from, savedPosition) {
    return { x: 0, y: 0 }
  }, mode: 'history', routes
});

router.beforeEach((to, from, next) => {
  if (to.name.includes("dashboard")) {
    if (!localStorage.token) {
      next('/');
    }
  }
  if (to.name.includes("admin")) {
    axios
      .get(url + "/auth/actual", {
        headers: { Authorization: `Bearer ${localStorage.token}` }
      })
      .then(res => {
        if (!res.data.data.admin) {
          next('/');
        }
      });
  }
  next();
});

new Vue({
  router,
  render: h => h(App)
}).$mount('#app');