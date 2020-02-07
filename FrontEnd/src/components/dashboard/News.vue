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
            <th scope="col">Título</th>
            <th scope="col">Descrição</th>
            <th scope="col">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr class="dashboard-news-search" v-for="novidade in user.posts" v-bind:key="novidade.id">
            <td>{{novidade.id}}</td>
            <td>{{novidade.title}}</td>
            <td>{{novidade.description}}</td>
            <td class="d-flex justify-content-around aign-items-center">
              <img src="/icon/edit.svg" class="icon" @click="editElement(novidade)" />
              <img src="/icon/delete.svg" class="icon" @click="deleteElement(novidade.id)" />
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <NewsForm :data="animal" :post="post" :association_id="user.id"></NewsForm>
  </div>
</template>

<script>
import NewsForm from "./form/NewsForm.vue";
import axios from "axios";
import SearchBar from "../layout/SearchBar.vue";
const { url } = require("../../../helper");
import JQuery from "jquery";
let $ = JQuery;

export default {
  components: {
    SearchBar,
    NewsForm
  },
  data() {
    return {
      animals: null,
      post: true,
      search: ".dashboard-news-search",
      animal: {
        id: null,
        title: null,
        description: null
      }
    };
  },
  props: ["user"],
  methods: {
    editElement(e) {
      this.post = false;
      this.animal.id = e.id;
      this.animal.title = e.title;
      this.animal.description = e.description;

      $("#newsModal").modal("show");
    },
    resetElement() {
      this.post = true;
      this.animal.id = null;
      this.animal.title = null;
      this.animal.description = null;

      $("#newsModal").modal("show");
    },
    deleteElement(e) {
      axios
        .delete(url + "/post/" + e, {
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