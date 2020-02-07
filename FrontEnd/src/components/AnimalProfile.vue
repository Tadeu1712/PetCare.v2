<template>
  <div
    class="modal fade"
    id="exampleModalCenter"
    tabindex="-1"
    role="dialog"
    aria-labelledby="exampleModalCenterTitle"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
          <div class="card">
            <img v-if="animal.image" v-bind:src="animal.image" class="card-img-top" />
            <img v-else src="/default-animal.jpg" class="card-img-top" />
            <div class="card-body">
              <div class="mb-0 d-flex justify-content-between align-items-center">
                <h3 class="mb-0">{{animal.name}}</h3>
                <span class="badge badge-pill badge-info">{{animal.status}}</span>
              </div>
              <p v-if="animal.association">
                <small class="mb-3 text-muted">{{animal.breed}}, {{animal.association.user.name}}</small>
              </p>

              <p v-else>
                <small class="mb-3 text-muted">{{animal.breed}}, {{partner.name}}</small>
              </p>

              <div class="d-flex justify-content-around">
                <div class>
                  <p class="mb-0 data data-number">{{animal.weight}}kg</p>
                  <p class="data data-text">Peso</p>
                </div>
                <div class>
                  <p class="mb-0 data data-number">{{getAge(animal.age)}}</p>
                  <p class="data data-text">Idade</p>
                </div>
                <div class>
                  <p class="mb-0 data data-number">{{animal.size}}</p>
                  <p class="data data-text">Porte</p>
                </div>
              </div>

              <p>{{animal.description}}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
const { external_url } = require("../../helper");

export default {
  name: "animalProfile",
  props: ["animal", "partner"],
  data() {
    return {
      url: external_url
    };
  },
  methods: {
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
.card {
  width: 100%;
  border: none;
  margin: 0;
}
.modal-body {
  width: 100%;

  padding: 0;
  position: relative;
}
.modal-dialog {
  max-width: 400px;
}
.close {
  position: absolute;
  right: 5px;
  top: 5px;
  z-index: 1;
}
.icon {
  width: 15px;
  height: 15px;
  margin-right: 1%;
}

.data {
  text-align: center;
}

.data-number {
  color: black;
  font-weight: bold;
}

.data-text {
  color: gray;
  font-size: 0.9em;
}
</style>
