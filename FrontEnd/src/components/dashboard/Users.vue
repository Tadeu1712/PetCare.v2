<template>
  <div class="container-fluid background">
    <div class="table-filters">
      <SearchBar :search="search" />
    </div>

    <div class="table-responsive">
      <button
        type="button"
        @click="resetElement()"
        class="btn btn-primary float-right mb-2 spmodal"
      >Adicionar</button>
      <table class="table shadow-sm p-3 mb-5 bg-white rounded">
        <thead class="thead-dark">
          <tr>
            <th scope="col">#</th>
            <th scope="col">Imagem</th>
            <th scope="col">Nome</th>
            <th scope="col">Email</th>
            <th scope="col">Contacto</th>
            <th scope="col">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr class="dashboard-users-search" v-for="user in users" v-bind:key="user.id">
            <td>{{user.id}}</td>
            <td>
              <img v-if="user.image" v-bind:src="user.image" class="profile-pic" />
              <img v-else src="/default-user.jpg" class="profile-pic" />
            </td>
            <td>{{user.user.name}}</td>
            <td>{{user.user.email}}</td>
            <td>{{user.phoneNumber}}</td>
            <td class="d-flex justify-content-around aign-items-center">
              <img src="/icon/edit.svg" class="icon" @click="editElement(user)" />
              <img src="/icon/delete.svg" class="icon" @click="deleteElement(user.id)" />
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <UserForm :data="user" :post="post"></UserForm>
  </div>
</template>

<script>
import UserForm from "./form/UserForm.vue";
import axios from "axios";
import SearchBar from "../layout/SearchBar.vue";
import JQuery from "jquery";
let $ = JQuery;
const { url } = require("../../../helper");

export default {
  components: {
    SearchBar,
    UserForm
  },
  data() {
    return {
      users: null,
      post: true,
      search: ".dashboard-users-search",
      user: {
        id: null,
        name: null,
        description: null,
        email: null,
        phone: null,
        address: null,
        iban: null,
        foundationDate: null
      }
    };
  },
  created: function() {
    axios
      .get(url + "/association/all", {
        headers: { Authorization: `Bearer ${localStorage.token}` }
      })
      .then(res => {
        this.users = res.data.data;
      });
  },
  methods: {
    editElement(e) {
      this.post = false;
      this.user.id = e.id;
      this.user.name = e.user.name;
      this.user.description = e.description;
      this.user.email = e.user.email;
      this.user.phone = e.phoneNumber;
      this.user.address = e.address;
      this.user.iban = e.iban;
      this.user.image = e.image;
      this.user.foundationDate = new Date(e.foundationDate)
        .toISOString()
        .substr(0, 10);

      $("#userModal").modal("show");
    },
    resetElement() {
      this.post = true;
      this.user.id = null;
      this.user.name = null;
      this.user.description = null;
      this.user.email = null;
      this.user.phone = null;
      this.user.address = null;
      this.user.iban = null;
      this.user.foundationDate = null;
      this.user.image = null;

      $("#userModal").modal("show");
    },
    deleteElement(e) {
      axios
        .delete(url + "/association/" + e, {
          headers: { Authorization: `Bearer ${localStorage.token}` }
        })
        .then(res => {
          location.reload();
        })
        .catch(err => {
          console.log("erro");
        });
    }
  }
};
</script>

<style scoped>
.background {
  background-color: rgba(248, 253, 255, 0.678);
  height: 100%;
}
.table-responsive {
  width: 80%;
  display: block;
  margin: 5% auto;
}
.table-filters {
  width: 100%;
  height: 80px;
  background: white;
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.2);
  display: flex;
  align-items: center;
  justify-content: center;
}
.profile-pic {
  width: 35px;
}
.icon {
  width: 20px;
  cursor: pointer;
}
</style>