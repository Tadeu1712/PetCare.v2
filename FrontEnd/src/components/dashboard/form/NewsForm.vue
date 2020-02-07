<template>
  <div class="modal fade" id="newsModal" role="dialog">
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

          <div class="form-group">
            <textarea
              v-model="data.description"
              class="form-control form-textarea"
              placeholder="Pequena descrição da notícia"
              rows="3"
            ></textarea>
          </div>

          <div class="d-flex justify-content-center">
            <button type="submit" class="btn btn-primary" @click="submitData()">Enviar</button>
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

export default {
  props: ["data", "post", "association_id"],
  methods: {
    submitData() {
      const { data, post } = this;

      if (post) {
        axios
          .post(
            url + "/post/create",
            {
              association_id: this.association_id,
              title: data.title,
              description: data.description,
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
      } else {
        axios
          .put(
            url + "/post/update",
            {
              id: data.id,
              association_id: this.association_id,
              title: data.title,
              description: data.description,
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
    }
  },
};
</script>

<style scoped>
@import "/css/dashboard.css";
</style>
