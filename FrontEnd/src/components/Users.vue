<template>
  <div class="container">
    <h3>Users:</h3>
    <table class="table">
      <thead>
        <tr>
          <th scope="col">Id</th>
          <th scope="col">Name</th>
          <th scope="col">Email</th>
          <th scope="col">City</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="user in users" v-bind:key="user.id">
          <th scope="row">{{user.id}}</th>
          <td>{{user.name}}</td>
          <td>{{user.email}}</td>
          <td>{{user.address.city}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import axios from "axios";
const { url } = require("../../helper");

export default {
  name: "Users",
  data() {
    return {
      users: null
    };
  },
  created: function() {
    axios.get(url + "/users").then(res => {
      this.users = res.data;
    });
  }
};
</script>

<style scoped>
h3 {
  margin-bottom: 5%;
}
</style>