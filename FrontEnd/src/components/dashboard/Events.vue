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
            <th scope="col">Localização</th>
            <th scope="col">Preço</th>
            <th scope="col">Início</th>
            <th scope="col">Fim</th>
            <th scope="col">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr class="dashboard-news-search" v-for="event in user.events" v-bind:key="event.id">
            <td>{{event.id}}</td>
            <td>{{event.title}}</td>
            <td>{{event.description}}</td>
            <td>{{event.location}}</td>
            <td>{{event.price > 0 ? event.price + '€' : "Grátis" }}</td>
            <td>{{new Date(event.dateInit).toLocaleDateString("pt-PT")}}</td>
            <td>{{new Date(event.dateEnd).toLocaleDateString("pt-PT")}}</td>

            <td class="d-flex justify-content-around aign-items-center">
              <img src="/icon/edit.svg" class="icon" @click="editElement(event)" />
              <img src="/icon/delete.svg" class="icon" @click="deleteElement(event.id)" />
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <EventForm :data="event" :post="post" :association_id="user.id"></EventForm>
  </div>
</template>

<script>
import EventForm from "./form/EventForm.vue";
import axios from "axios";
import SearchBar from "../layout/SearchBar.vue";
const { url } = require("../../../helper");
import JQuery from "jquery";
let $ = JQuery;

export default {
  components: {
    SearchBar,
    EventForm
  },
  data() {
    return {
      post: true,
      search: ".dashboard-events-search",
      event: {
        id: null,
        title: null,
        description: null,
        localization: null,
        price: null,
        init: null,
        end: null
      }
    };
  },
  props: ["user"],
  methods: {
    editElement(e) {
      this.post = false;
      this.event.id = e.id;
      this.event.title = e.title;
      this.event.description = e.description;
      this.event.location = e.location;
      this.event.type = e.type;
      this.event.image = e.image;
      this.event.price = e.price;
      this.event.init = new Date(e.dateInit).toISOString().substr(0, 10);
      this.event.end = new Date(e.dateEnd).toISOString().substr(0, 10);

      $("#eventsModal").modal("show");
    },
    resetElement() {
      this.post = true;
      this.event.id = null;
      this.event.title = null;
      this.event.type = null;
      this.event.description = null;
      this.event.location = null;
      this.event.price = null;
      this.event.init = null;
      this.event.end = null;
      this.event.image = null;

      $("#eventsModal").modal("show");
    },
    deleteElement(e) {
      axios
        .delete(url + "/events/" + e, {
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