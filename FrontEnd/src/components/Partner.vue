<template>
  <div v-if="partner" class="mb-5">
    <Header title description image="background-purple.svg" :logo="partner.image"></Header>
    <center>
      <div v-show="partner" class="container partner-title">
        <h1>{{ partner.user.name }}</h1>
        <p>{{ new Date(partner.foundationDate).toLocaleDateString('pt-PT', dateOptions) }}</p>
      </div>

      <div class="d-flex justify-content-center">
        <div class="mx-3">
          <p class="mb-0 data data-number">{{animals.length}}</p>
          <p class="data data-text">Animais</p>
        </div>

        <div class="mx-3 separation">
          <p class="mb-0 data data-number">{{events.length}}</p>
          <p class="data data-text">Eventos</p>
        </div>

        <div class="mx-3">
          <p class="mb-0 data data-number">{{news.length}}</p>
          <p class="data data-text">Novidades</p>
        </div>
      </div>
    </center>

    <div class="container">
      <ul
        class="nav nav-pills mb-3 d-flex justify-content-around margin"
        id="pills-tab"
        role="tablist"
      >
        <li class="nav-item">
          <a
            class="nav-link active"
            id="pills-about-tab"
            data-toggle="pill"
            href="#pills-about"
            role="tab"
            aria-controls="pills-about"
            aria-selected="true"
          >Sobre</a>
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            id="pills-animal-tab"
            data-toggle="pill"
            href="#pills-animal"
            role="tab"
            aria-controls="pills-animal"
            aria-selected="false"
          >Animais</a>
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            id="pills-profile-tab"
            data-toggle="pill"
            href="#pills-profile"
            role="tab"
            aria-controls="pills-profile"
            aria-selected="false"
          >Eventos</a>
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            id="pills-contact-tab"
            data-toggle="pill"
            href="#pills-contact"
            role="tab"
            aria-controls="pills-contact"
            aria-selected="false"
          >Novidades</a>
        </li>
      </ul>
      <div class="tab-content d-flex justify-content-center" id="pills-tabContent">
        <div
          class="container tab-pane fade show active"
          id="pills-about"
          role="tabpanel"
          aria-labelledby="pills-about-tab"
        >
          <div class="row">
            <div class="col-lg-8 col-sm-12">
              <h1>Quem somos?</h1>
              <p>{{partner.description}}</p>
            </div>
            <div class="col-lg-4 col-sm-12">
              <h1>Contactar</h1>

              <div>
                <img src="/icon/email.svg" class="icon" />
                <span>{{partner.user.email}}</span>
              </div>

              <div>
                <img src="/icon/phone.svg" class="icon" />
                <span>{{partner.phoneNumber}}</span>
              </div>

              <br />
              <span class="font-weight-bold"></span>
              <p>{{partner.address}}</p>

              <div>
                <img src="/icon/bank.svg" class="icon" />
                <span>{{partner.iban}}</span>
              </div>
            </div>
          </div>
        </div>
        <div
          class="tab-pane fade show"
          id="pills-animal"
          role="tabpanel"
          aria-labelledby="pills-animal-tab"
        >
          <div class="container">
            <div class="row">
              <div
                class="col-sm-4"
                v-for="animal in animals"
                v-bind:key="animal.id"
                data-toggle="modal"
                data-target="#exampleModalCenter"
              >
                <div class="card margin shadow my-5 animal-card" @click="storeID(animal)">
                  <img v-bind:src="animal.image" class="card-img-top" />
                  <div class="card-body">
                    <div class="mb-0 d-flex justify-content-between align-items-center">
                      <h5 class="card-title mb-0">{{animal.name}}</h5>
                      <span class="badge badge-pill badge-info">{{animal.status}}</span>
                    </div>
                    <p class="card-text">{{animal.type}}, {{animal.breed}}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div
          class="tab-pane fade"
          id="pills-profile"
          role="tabpanel"
          aria-labelledby="pills-profile-tab"
        >
          <div class="container card mb-3 margin" v-for="event in events" v-bind:key="event.id">
            <div class="row">
              <div class="col-md-4">
                <img
                  v-bind:src="event.image"
                  class="card-img"
                />
              </div>
              <div class="col-md-8">
                <div class="card-body">
                  <h5 class="card-title">{{event.title}}</h5>
                  <p class="card-text">{{event.description}}</p>
                  <p class="card-text">{{event.location}}</p>
                  <p
                    class="card-text"
                  >{{new Date(event.dateInit).toLocaleDateString()}} to {{new Date(event.dateEnd).toLocaleDateString()}}</p>
                  <p class="card-text">{{event.price}} €</p>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div
          class="tab-pane fade"
          id="pills-contact"
          role="tabpanel"
          aria-labelledby="pills-contact-tab"
        >
          <div class="container mb-3 margin" v-for="post in news" v-bind:key="post.id">
            <div class="card-body">
              <h5 class="card-title">{{post.title}}</h5>
              <p class="card-text ml-3">{{post.description}}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-if="data">
      <AnimalProfile :animal="data" :partner="partner.user" ></AnimalProfile>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Header from "./layout/Header.vue";
import AnimalProfile from "./AnimalProfile.vue";

const { url, external_url } = require("../../helper");

export default {
  name: "partner",
  components: {
    Header,
    AnimalProfile
  },
  data() {
    return {
      partner: null,
      animals: null,
      events: null,
      data: null,
      news: null,
      url: external_url,
      dateOptions: {
        year: "numeric",
        month: "long",
        day: "numeric"
      }
    };
  },
  mounted() {
    axios.get(url + "/association/" + this.$route.params.id).then(res => {
      this.partner = res.data.data;
      this.animals = res.data.data.animals;
      this.events = res.data.data.events;
      this.news = res.data.data.posts;
    });
  },
  methods: {
    storeID(animal) {
      this.data = animal;
    }
  }
};
</script>

<style scoped>
.animal-card {
  cursor: pointer;
}
.separation {
  padding: 0 30px;
  border-left: 1px solid lightgray;
  border-right: 1px solid lightgray;
}

.nav-link {
  color: #96009b;
}

.nav-pills .nav-link.active,
.nav-pills .show > .nav-link {
  background-color: #96009b;
  color: white;
}

.nav-link:hover {
  background-color: #97069c63;
  color: white;
}

.icon {
  width: 25px;
  height: 25px;
  margin-right: 2%;
}

.container {
  margin-top: 20px;
}

.partner-title {
  margin-top: 90px;
}

.data {
  font-weight: bold;
}

.data-number {
  color: #96009b;
}

.data-text {
  color: gray;
  font-size: 0.9em;
}
</style>
