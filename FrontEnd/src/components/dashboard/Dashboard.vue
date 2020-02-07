<template>
  <div class="wrapper" v-if="user">
    <div class="sidebar">
      <Sidebar :admin="admin"></Sidebar>
    </div>

    <div class="container-fluid">
      <router-view :user="user" />
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Sidebar from "./Sidebar.vue";
const { url } = require("../../../helper");

export default {
  name: "dashboard",
  components: {
    Sidebar
  },
  data() {
    return {
      user: null,
      admin: null,
    };
  },
  created() {
    axios
      .get(url + "/auth/actual", {
        headers: { Authorization: `Bearer ${localStorage.token}` }
      })
      .then(res => {
        this.user = res.data.data;
        this.admin = res.data.data.admin;
      })
      .catch(err => {
        console.log("erro");
      });
  }
};
</script>

<style scoped>
.wrapper {
  display: flex;
  width: 100%;
}
.container-fluid {
  padding: 0;
}
</style>