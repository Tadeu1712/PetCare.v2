<template>
  <div class="background" v-if="search">
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
        <div
          :style="style(animal.image)"
          class="lost-card my-3 lost-search"
          v-for="animal in animals"
          v-bind:key="animal.id"
        >
          <div class="card-initial">
            <div class="card-info">
              <div class="lost-data">
                <h3 class="lost-name">{{animal.title}}</h3>

                <div class="lost-information">
                  <div class="info lost-section">
                    <div class="info-icon">
                      <img src="/icon/location.svg" class="icon" />
                      <p class="info lost-address">{{animal.location}}</p>
                    </div>
                    <div class="info-icon">
                      <img src="/icon/date.svg" class="icon" />
                      <p
                        class="info lost-date"
                      >{{new Date(animal.date).toLocaleDateString("pt-PT")}}</p>
                    </div>
                  </div>
                </div>
              </div>

              <div class="lost-description">
                {{animal.description}}
                <div class="mt-3">
                  <div class="contact-icon">
                    <img src="/icon/contact.svg" class="icon" />
                    <p class="my-0 lost-phone">{{animal.contact}}</p>
                  </div>
                </div>
              </div>
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

const { url } = require("../../helper");

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
    axios.get(url + "/lost/all").then(res => {
      this.animals = res.data.data;
    });
  },
  methods: {
    style(image) {
      return "background-image: url(" + image + ");";
    }
  }
};
</script>

<style scoped>
.lost-name {
  font-size: 1.5em;
}
.lost-header {
  position: relative;
  margin-bottom: 250px;
}
.info-icon {
  display: flex;
  align-items: center;
  justify-content: center;
}

.lost-container {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  justify-content: space-around;
}

.lost-section {
  display: flex;
  align-items: center;
  justify-content: space-around;
}

.contact-icon {
  display: flex;
  justify-content: center;
}

.info {
  margin: 0 auto;
}

.lost-card {
  width: 30%;
  height: 250px;
  min-width: 280px;
  background-image: url("https://s3.amazonaws.com/cdn-origin-etr.akc.org/wp-content/uploads/2017/11/12234558/Chinook-On-White-03.jpg");
  background-size: cover;
  background-position: center;
}
.lost-description {
  display: none;
}
.icon {
  width: 20px;
  height: 20px;
  margin: 0 3px;
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

.lost-data {
  background: rgba(0, 0, 0, 0.5);
}
.card-initial:hover {
  background: rgba(0, 0, 0, 0.5);
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
