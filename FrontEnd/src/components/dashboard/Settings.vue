<template>
  <div>
    <div class="jumbotron jumbotron-fluid homepage">
      <div class="container-fluid d-flex justify-content-between align-items-center">
        <div>
          <h1 class="welcome welcome-message">
            Bem Vindo,
            <span class="bold">{{user.user.name}}</span>
          </h1>

          <p class="welcome welcome-question">PetCare</p>
        </div>
        <center class="hide">
          <div class="d-flex justify-content-between">
            <div class="data d-flex justify-content-around align-items-center">
              <span class="bold welcome-message">{{user.animals.length}}</span>
              <img src="/icon/animals.svg" class="icon" />
            </div>

            <div class="data d-flex justify-content-around align-items-center">
              <span class="bold welcome-message">{{user.events.length}}</span>
              <img src="/icon/events.svg" class="icon" />
            </div>

            <div class="data d-flex justify-content-around align-items-center">
              <span class="bold welcome-message">{{user.posts.length}}</span>
              <img src="/icon/news.svg" class="icon" />
            </div>
          </div>
        </center>
      </div>
    </div>

    <div class="container-fluid background">
      <div class="content">
        <div class="section public-section shadow-sm p-3 mb-5 bg-white rounded">
          <img :src="user.image" class="profile-image rounded" />
          <h1>{{user.user.name}}</h1>
        </div>
        <div class="section private-section shadow-sm p-3 mb-5 bg-white rounded">
          <div class="form-row">
            <div class="form-group col-md-6">
              <label for="inputAddress2">IBAN</label>
              <input
                v-model="user.iban"
                type="text"
                class="form-control"
                id="inputAddress2"
                placeholder="PT 5020194012301293"
              />
            </div>
            <div class="form-group col-md-6">
              <label for="inputFoundation">Fundação</label>
              <input
                v-model="foundation"
                type="date"
                class="form-control"
                id="inputFoundation"
                placeholder="Fundação"
              />
            </div>
          </div>
          <div class="form-row">
            <div class="form-group col-md-6">
              <label for="inputAddress">Telefone</label>
              <input
                v-model="user.phoneNumber"
                type="text"
                class="form-control"
                id="inputAddress"
                placeholder="+353927839123"
              />
            </div>
            <div class="form-group col-md-6">
              <label for="inputAddress">Address</label>
              <input
                v-model="user.address"
                type="text"
                class="form-control"
                id="inputAddress"
                placeholder="1234 Main St"
              />
            </div>
          </div>

          <div class="form-group">
            <label for="exampleFormControlTextarea1">Descrição</label>
            <textarea
              v-model="user.description"
              class="form-control"
              id="exampleFormControlTextarea1"
              rows="2"
            ></textarea>
          </div>

          <button @click="submitData()" type="submit" class="btn btn-primary">Atualizar</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
const { url } = require("../../../helper");
import axios from "axios";

export default {
  props: ["user"],
  data() {
    return {
      foundation: null
    };
  },
  methods: {
    submitData() {
      const { user } = this;
      const vm = this;
      axios
        .put(
          url + "/association/update",
          {
            id: user.id,
            iban: user.iban,
            address: user.address,
            phoneNumber: user.phoneNumber,
            description: user.description,
            foundationDate: this.foundation,
            user_id: user.user.id,
            image: user.image
          },
          {
            headers: { Authorization: `Bearer ${localStorage.token}` }
          }
        )
        .then(function(res) {
          vm.message = "success";
          location.reload();
        })
        .catch(function(e) {
          console.log("error");
        });
    }
  },
  mounted() {
    this.foundation = new Date(this.user.foundationDate)
      .toISOString()
      .substr(0, 10);
  }
};
</script>

<style scoped>
.background {
  background-color: rgba(248, 253, 255, 0.678);
  height: 100%;
}
.profile-image {
  width: 60%;
  min-width: 100px;
  max-width: 300px;
  margin: 5% auto;
  display: block;
}
.jumbotron {
  height: 220px;
  background-image: url("/background/dashboard.png");
  background-attachment: fixed;
  background-size: cover;
  color: white;
  margin-bottom: 0;
}
.public-section {
  width: 30%;
}
.private-section {
  width: 60%;
}

.section {
  background-color: white;
  margin: 2% 0;
}
.content {
  margin: auto;
  display: flex;
  justify-content: space-around;
  flex-wrap: wrap;
}

.welcome-message {
  font-size: 1.5em;
}

.welcome-question {
  font-size: 1em;
}

.bold {
  font-weight: bold;
}

.icon {
  width: 20px;
  height: 20px;
  margin-left: 5px;
}

.data-icon {
  width: 25px;
  height: 25px;
  margin-right: 2%;
}

.data {
  margin: 0 10px;
}

@media (max-width: 768px) {
  .public-section,
  .private-section {
    width: 100%;
  }
  .hide {
    display: none;
  }
}
</style>
