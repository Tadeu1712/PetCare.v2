<template>
  <div>
    <Feedback v-if="message" :message="message" />
    <div class="lost-form">
      <div class="form">
        <div class="form-row">
          <div class="form-group col-sm-6">
            <input v-model="title" required type="text" class="form-control" placeholder="Título" />
          </div>
          <div class="form-group col-sm-6">
            <input
              v-model="address"
              required
              type="text"
              class="form-control"
              placeholder="Localização"
            />
          </div>
        </div>
        <div class="form-row">
          <div class="form-group col-sm-6">
            <input v-model="date" required type="date" class="form-control" placeholder="Data" />
          </div>
          <div class="form-group col-sm-6">
            <input
              v-model="phone"
              type="tel"
              pattern="(^291[0-9]{6})|(^9{1}[0-9]{8})"
              class="form-control"
              placeholder="Contacto"
            />
          </div>
        </div>

        <div class="form-group">
          <textarea
            v-model="description"
            required
            class="form-control"
            placeholder="Descrição"
            rows="2"
          ></textarea>
        </div>
        <image-compressor :done="getFiles" :scale="scale" :quality="quality"></image-compressor>

        <img v-if="file" :src="file.base64" class="animal-image my-2" />

        <center>
          <button type="submit" @click="submitData()" class="btn btn-light">Submit</button>
        </center>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Feedback from "../../layout/FPFeedback.vue";
import imageCompressor from "vue-image-compressor";
const { url } = require("../../../../helper");

export default {
  data() {
    return {
      title: null,
      description: null,
      address: null,
      email: null,
      phone: null,
      date: null,
      message: null,
      file: "",
      scale: 60,
      quality: 30
    };
  },
  components: {
    Feedback,
    imageCompressor
  },
  methods: {
    submitData() {
      const vm = this;
      axios
        .post(
          url + "/lost/create",
          {
            title: this.title,
            description: this.description,
            contact: this.phone,
            location: this.address,
            date: this.date,
            image: this.file.base64
          },
          {
            headers: { Authorization: `Bearer ${localStorage.token}` }
          }
        )
        .then(function(res) {
          location.reload();
        })
        .catch(function(e) {
          console.log("error");
        });
    },
    getFiles(obj) {
      this.file = obj.compressed;
    }
  }
};
</script>

<style scoped>
.animal-image{
  width: 30px;
}
.lost-form {
  position: absolute;
  top: 250px;
  left: 0;
  right: 0;
  margin: auto;
  @apply --container;
  max-width: 700px;
}
.form-control {
  resize: none;
}
.btn {
  padding: 1% 10%;
}
.form {
  background: rgba(0, 0, 0, 0.5);
  padding: 2%;
}
</style>
