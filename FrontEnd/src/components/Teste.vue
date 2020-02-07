<template>
  <div v-if="search">
    <div class="lost-header">
      <Header
        title="Perdidos"
        description="Animais encontrados pelas associações"
        image="background-black.svg"
        class="mb-5"
      ></Header>

      <LostForm />
    </div>
    <div class="container h-100 my-5">
      <div class="d-flex justify-content-center">
        <SearchBar :search="search" />
      </div>
    </div>
    <div class="container">
      <div class="row lost-container my-5">
        <div class="lost-card my-3" v-for="animal in animals" v-bind:key="animal.id">
          <div class="card-initial">
            <div class="card-info">
              <div class="lost-data">
                <h3 class="lost-name">Bolinhas</h3>
                <p class="info lost-address">Funchal, Mercado</p>
                <p class="info lost-date">03-03-2003</p>
              </div>

              <div
                class="lost-description"
              >Lorem ipsum dolor, sit amet consectetur adipisicing elit. Corrupti dignissimos praesentium saepe ea ad aliquam voluptatibus et esse deleniti distincti</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Header from "./layout/Header.vue";
import SearchBar from "./layout/SearchBar.vue";
import LostForm from "./dashboard/form/LostForm.vue";

export default {
  name: "teste",
  components: {
    Header,
    SearchBar,
    LostForm
  },
  data() {
    return {
      animals: null,
      search: ".lost-search"
    };
  },
  created: function() {
    axios.get("https://jsonplaceholder.typicode.com/users").then(res => {
      this.animals = res.data;
    });
  }
};
</script>

<style scoped>
.lost-header {
  position: relative;
  margin-bottom: 190px;
}

.lost-container {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  justify-content: space-between;
}

.info {
  margin: 0 auto;
}

.lost-card {
  width: 30%;
  height: 250px;
  background-image: url("https://www.humanesociety.org/sites/default/files/styles/1240x698/public/2019/02/dog-451643.jpg?h=bf654dbc&itok=MQGvBmuo");
  background-size: cover;
  background-position: center;
}
.lost-description {
  display: none;
}

.card-initial {
  height: 100%;
  width: 100%;
  background: rgba(0, 0, 0, 0.3);
  color: white;
  font-weight: bold;
  text-align: center;
  position: relative;
  transition: 0.5s ease;
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.3);
}
.card-initial:hover {
  background: rgba(0, 0, 0, 0.513);
}
.card-initial:hover .lost-description {
  display: block;
}

.card-initial:hover .lost-data {
  display: none;
}

.card-initial:hover .card-info {
  position: absolute;
  bottom: 50%;
  -webkit-transform: translateY(50%);
  transform: translateY(50%);
}

.card-info {
  position: absolute;
  bottom: 0;
  right: 0;
  left: 0;
  margin: auto;
}
.flip-card-front {
  background-color: transparent;
}
.flip-card-back {
  background-color: white;
  color: black;
  transform: rotateY(180deg);
}
.flip-card:hover .flip-card-inner,
.flip-card:active .flip-card-inner {
  transform: rotateY(-180deg);
}
.logo {
  width: 100%;
  height: 100%;
}

@media only screen and (max-width: 580px) {
  .lost-header {
    margin-bottom: 350px;
  }
}
</style>
