<template>
  <div class="modal fade" id="eventsModal" role="dialog">
    <div class="modal-dialog modal-dialog-centered dashboard-modal" role="document">
      <div class="modal-content form-background">
        <div class="modal-header">
          <h4 class="modal-title">Formulário</h4>
          <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        </div>
        <div class="modal-body">
          <div class="form-group">
            <input v-model="data.title" type="text" class="form-control" placeholder="Nome" />
          </div>

          <div class="form-row">
            <div class="form-group col-md-5">
              <select class="form-control" v-model="data.type" name="type">
                <option :value="null" disabled>Tipo</option>
                <option value="Caridade">Caridade</option>
                <option value="Concentração">Concentração</option>
                <option value="Campanha">Campanha</option>
              </select>
            </div>
            <div class="form-group col-md-6">
              <input
                v-model="data.location"
                type="text"
                class="form-control"
                placeholder="Localização"
              />
            </div>
          </div>

          <div class="form-group">
            <input
              v-model="data.price"
              type="number"
              min="0"
              class="form-control"
              placeholder="Preço (Coloque 0 para grátis)"
            />
          </div>

          <div class="form-row">
            <div class="form-group col-md-6">
              <input v-model="data.init" class="form-control" type="date" name="start" />
            </div>
            <div class="form-group col-md-6">
              <input v-model="data.end" class="form-control" type="date" name="end" />
            </div>
          </div>

          <div class="form-group">
            <textarea
              v-model="data.description"
              class="form-control form-textarea"
              placeholder="Pequena descrição do evento"
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
  components: {
    imageCompressor
  },
  data() {
    return {
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
            url + "/events/create",
            {
              association_id: this.association_id,
              title: data.title,
              description: data.description,
              location: data.location,
              type: data.type,
              price: data.price,
              image: file.base64,
              dateInit: data.init,
              dateEnd: data.end
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
            url + "/events/update",
            {
              id: data.id,
              association_id: this.association_id,
              title: data.title,
              description: data.description,
              location: data.location,
              type: data.type,
              price: data.price,
              image: data.image,
              dateInit: data.init,
              dateEnd: data.end
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
  }
};
</script>

<style scoped>
@import "/css/dashboard.css";
</style>
