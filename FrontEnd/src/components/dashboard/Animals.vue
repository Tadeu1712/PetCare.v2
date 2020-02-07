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
            <th scope="col">Nome</th>
            <th scope="col">Espécie</th>
            <th scope="col">Caraterísticas</th>
            <th scope="col">Status</th>
            <th scope="col">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr
            class="dashboard-animals-search"
            v-for="animal in user.animals"
            v-bind:key="animal.id"
          >
            <td>{{animal.id}}</td>
            <td>{{animal.name}}</td>
            <td>{{animal.type}}, {{animal.breed}}</td>
            <td>{{animal.size}} | {{animal.weight}}kg | {{getAge(animal.age)}}anos</td>
            <td>{{animal.status}}</td>
            <td class="d-flex justify-content-around aign-items-center">
              <img src="/icon/edit.svg" class="icon" @click="editElement(animal)" />
              <img src="/icon/delete.svg" class="icon" @click="deleteElement(animal.id)" />
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <AnimalForm :data="animal" :post="post" :association_id="user.id"></AnimalForm>
  </div>
</template>

<script>
import JQuery from "jquery";
import axios from "axios";
import AnimalForm from "./form/AnimalForm.vue";
import SearchBar from "../layout/SearchBar.vue";

const { url } = require("../../../helper");
let $ = JQuery;

export default {
  components: {
    SearchBar,
    AnimalForm
  },
  data() {
    return {
      post: true,
      search: ".dashboard-animals-search",
      animal: {
        id: null,
        name: null,
        breed: null,
        type: null,
        weight: null,
        size: null,
        age: null,
        status: null,
        description: null,
        energy: null,
        chill: null,
        funny: null,
        troublemaker: null,
        image: null
      }
    };
  },
  props: ["user"],
  methods: {
    editElement(e) {
      this.post = false;
      this.animal.id = e.id;
      this.animal.name = e.name;
      this.animal.breed = e.breed;
      this.animal.type = e.type;
      this.animal.weight = e.weight;
      this.animal.size = e.size;
      this.animal.age = new Date(e.age).toISOString().substr(0, 10);
      this.animal.status = e.status;
      this.animal.description = e.description;
      this.animal.energy = e.energy;
      this.animal.chill = e.chill;
      this.animal.funny = e.funny;
      this.animal.troublemaker = e.troubleMaker;
      this.animal.image = e.image;

      $("#animalModal").modal("show");
    },
    resetElement() {
      this.post = true;
      this.animal.id = null;
      this.animal.name = null;
      this.animal.breed = null;
      this.animal.type = null;
      this.animal.weight = null;
      this.animal.size = null;
      this.animal.age = null;
      this.animal.status = null;
      this.animal.description = null;
      this.animal.energy = null;
      this.animal.chill = null;
      this.animal.funny = null;
      this.animal.troublemaker = null;
      this.animal.image = null;

      $("#animalModal").modal("show");
    },
    deleteElement(e) {
      axios
        .delete(url + "/animal/" + e, {
          headers: { Authorization: `Bearer ${localStorage.token}` }
        })
        .then(res => {
          location.reload();
        })
        .catch(err => {
          console.log("erro");
        });
    },
    getAge(dateString) {
      var today = new Date();
      var birthDate = new Date(dateString);
      var age = today.getFullYear() - birthDate.getFullYear();
      var m = today.getMonth() - birthDate.getMonth();
      if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
      }
      return age;
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
.icon {
  width: 20px;
  cursor: pointer;
}
</style>