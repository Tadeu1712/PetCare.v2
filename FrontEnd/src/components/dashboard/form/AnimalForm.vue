<template>
  <div class="modal fade" id="animalModal" role="dialog">
    <div class="modal-dialog modal-dialog-centered dashboard-modal" role="document">
      <div class="modal-content form-background">
        <div class="modal-header">
          <h4 class="modal-title">Formulário</h4>
          <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        </div>
        <div class="modal-body">
          <div class="d-flex justify-content-center my-2">
            <div class="custom-control custom-radio custom-control-inline">
              <input
                value="Cão"
                type="radio"
                v-model="data.type"
                id="specie1"
                name="specie"
                class="custom-control-input"
              />
              <label class="custom-control-label" for="specie1">Cão</label>
            </div>
            <div class="custom-control custom-radio custom-control-inline">
              <input
                value="Gato"
                v-model="data.type"
                type="radio"
                id="specie2"
                name="specie"
                class="custom-control-input"
              />
              <label class="custom-control-label" for="specie2">Gato</label>
            </div>
          </div>

          <div class="d-flex justify-content-center my-2">
            <div class="custom-control custom-radio custom-control-inline">
              <input
                value="Adotado"
                type="radio"
                v-model="data.status"
                name="status"
                id="status1"
                class="custom-control-input"
              />
              <label class="custom-control-label" for="status1">Adotado</label>
            </div>
            <div class="custom-control custom-radio custom-control-inline">
              <input
                value="Adoção"
                v-model="data.status"
                type="radio"
                name="status"
                id="status2"
                class="custom-control-input"
              />
              <label class="custom-control-label" for="status2">Adoção</label>
            </div>
            <div class="custom-control custom-radio custom-control-inline">
              <input
                value="Perdido"
                v-model="data.status"
                type="radio"
                name="status"
                id="status3"
                class="custom-control-input"
              />
              <label class="custom-control-label" for="status3">Perdido</label>
            </div>
          </div>

          <div class="form-row">
            <div class="form-group col-md-6">
              <input v-model="data.name" type="text" class="form-control" placeholder="Nome" />
            </div>
            <div class="form-group col-md-6">
              <input v-model="data.breed" type="text" class="form-control" placeholder="Raça" />
            </div>
          </div>

          <div class="form-row">
            <div class="form-group col-md-4">
              <select class="form-control" v-model="data.size" name="size">
                <option :value="null" disabled>Porte</option>
                <option value="Grande">Grande</option>
                <option value="Médio">Médio</option>
                <option value="Pequeno">Pequeno</option>
              </select>
            </div>
            <div class="form-group col-md-4">
              <input v-model="data.weight" type="text" class="form-control" placeholder="Peso (kg)" />
            </div>
            <div class="form-group col-md-4">
              <input v-model="data.age" type="date" class="form-control" placeholder="Idade (anos)" />
            </div>
          </div>

          <div class="form-row">
            <div class="form-group col-md-6">
              <input
                v-model="data.funny"
                type="text"
                class="form-control"
                placeholder="Engraçado (0-100)"
              />
            </div>
            <div class="form-group col-md-6">
              <input
                v-model="data.energy"
                type="text"
                class="form-control"
                placeholder="Energia (0-100)"
              />
            </div>
          </div>
          <div class="form-row">
            <div class="form-group col-md-6">
              <input
                v-model="data.chill"
                type="text"
                class="form-control"
                placeholder="Calmo (0-100)"
              />
            </div>
            <div class="form-group col-md-6">
              <input
                v-model="data.troublemaker"
                type="text"
                class="form-control"
                placeholder="Travesso (0-100)"
              />
            </div>
          </div>

          <div class="form-group">
            <textarea
              v-model="data.description"
              class="form-control form-textarea"
              placeholder="Pequena descrição do animal"
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
  props: ["data", "post", "association_id"],
  data() {
    return {
      file: "",
      scale: 60,
      quality: 30
    };
  },
  components: {
    imageCompressor
  },
  created() {
    this.file = this.data.image;
  },
  methods: {
    submitData() {
      const { data, post, file } = this;

      if (post) {
        axios
          .post(
            url + "/animal/create",
            {
              name: data.name,
              breed: data.breed,
              type: data.type,
              weight: data.weight,
              size: data.size,
              age: data.age,
              status: data.status,
              description: data.description,
              energy: data.energy,
              funny: data.funny,
              chill: data.chill,
              troublemaker: data.troublemaker,
              image: file.base64,
              association_id: this.association_id
            },
            {
              headers: {
                Authorization: `Bearer ${localStorage.token}`
              }
            }
          )
          .then(function(res) {
            location.reload();
          })
          .catch(function(e) {
            alert("error");
          });
      } else {
        axios
          .put(
            url + "/animal/update",
            {
              id: data.id,
              name: data.name,
              breed: data.breed,
              type: data.type,
              weight: data.weight,
              size: data.size,
              age: data.age,
              status: data.status,
              description: data.description,
              energy: data.energy,
              funny: data.funny,
              chill: data.chill,
              image: data.image,
              troublemaker: data.troublemaker,
              association_id: this.association_id
            },
            {
              headers: { Authorization: `Bearer ${localStorage.token}` }
            }
          )
          .then(function(res) {
            location.reload();
          })
          .catch(function(e) {
            alert("error");
          });
      }
    },
    getFiles(obj) {
      this.file = obj.compressed;
      this.data.image = obj.compressed.base64;
    }
  }
};
</script>

<style scoped>
@import "/css/dashboard.css";
</style>
