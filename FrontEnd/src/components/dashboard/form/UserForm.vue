<template>
  <div class="modal fade" id="userModal" role="dialog">
    <div class="modal-dialog modal-dialog-centered dashboard-modal" role="document">
      <div class="modal-content form-background">
        <div class="modal-header">
          <h4 class="modal-title">Formulário</h4>
          <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        </div>
        <div class="modal-body">
          <div v-if="post" class="form-row">
            <div class="form-group col-md-6">
              <input v-model="data.name" type="text" class="form-control" placeholder="Nome" />
            </div>
            <div class="form-group col-md-6">
              <input
                v-model="data.foundationDate"
                type="date"
                class="form-control"
                placeholder="Fundação"
              />
            </div>
          </div>

          <div v-if="!post" class="form-row">
            <div class="form-group col-md-6">
              <input
                type="tel"
                class="form-control"
                placeholder="Telefone"
                pattern="(^291[0-9]{6})|(^9{1}[0-9]{8})"
                name="phone"
                v-model="data.phone"
              />
            </div>
            <div class="form-group col-md-6">
              <input
                v-model="data.foundationDate"
                type="date"
                class="form-control"
                placeholder="Fundação"
              />
            </div>
          </div>

          <div v-if="post" class="form-row">
            <div class="form-group col-md-6">
              <input v-model="data.email" type="email" class="form-control" placeholder="Email" />
            </div>
            <div class="form-group col-md-6">
              <input
                type="tel"
                class="form-control"
                placeholder="Telefone"
                pattern="(^291[0-9]{6})|(^9{1}[0-9]{8})"
                name="phone"
                v-model="data.phone"
              />
            </div>
          </div>

          <div v-if="post" class="form-group">
            <input
              v-model="data.password"
              type="password"
              class="form-control"
              placeholder="Password"
            />
          </div>

          <div class="form-group">
            <input
              id="iban"
              v-model="data.iban"
              type="text"
              name="iban"
              class="form-control"
              placeholder="IBAN"
            />
          </div>

          <div class="form-group">
            <input
              v-model="data.address"
              type="text"
              name="address"
              class="form-control"
              placeholder="Morada"
            />
          </div>

          <div class="form-group">
            <textarea
              v-model="data.description"
              class="form-control form-textarea"
              placeholder="Pequena descrição da notícia"
              rows="3"
            ></textarea>
          </div>

          <image-compressor :done="getFiles" :scale="scale" :quality="quality"></image-compressor>

          <img v-if="!post" :src="data.image" class="animal-image my-2" />
          <img v-if="file && post" :src="file.base64" class="animal-image my-2" />

          <div class="d-flex justify-content-center">
            <button type="submit" @click="submitData()" class="btn btn-primary">Enviar</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import JQuery from "jquery";
let $ = JQuery;
const { url } = require("../../../../helper");
import imageCompressor from "vue-image-compressor";

export default {
  props: ["data", "post"],
  components: {
    imageCompressor
  },
  data() {
    return {
      name: null,
      description: null,
      email: null,
      phone: null,
      address: null,
      iban: null,
      file: "",
      scale: 60,
      quality: 30
    };
  },
  methods: {
    submitData() {
      const { data, post, file } = this;

      if (post) {
        axios
          .post(
            url + "/auth/register",
            {
              description: data.name,
              phoneNumber: data.phone,
              address: data.address,
              iban: data.iban,
              foundationDate: data.foundationDate,
              image: file.base64,
              user: {
                name: data.name,
                email: data.email,
                password: data.password
              }
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
      } else {
        axios
          .put(
            url + "/association/update",
            {
              id: data.id,
              iban: data.iban,
              address: data.address,
              phoneNumber: data.phone,
              description: data.description,
              foundationDate: data.foundationDate,
              user_id: data.user_id,
              image: data.image
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
      }
    },
    getFiles(obj) {
      this.file = obj.compressed;
      this.data.image = obj.compressed.base64;
    }
  },
  mounted() {
    document.getElementById("iban").addEventListener("input", function(e) {
      var target = e.target,
        position = target.selectionEnd,
        length = target.value.length;

      target.value = target.value
        .replace(/[^\dA-Z]/g, "")
        .replace(/(.{4})/g, "$1 ")
        .trim();
      target.selectionEnd = position +=
        target.value.charAt(position - 1) === " " &&
        target.value.charAt(length - 1) === " " &&
        length !== target.value.length
          ? 1
          : 0;
    });
  }
};
</script>

<style scoped>
@import "/css/dashboard.css";
</style>
