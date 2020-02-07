<template>
      <div class="searchbar">
        <input
          id="search-input"
          class="search_input"
          type="text"
          name="search"
          placeholder="Search..."
        />
      </div>
</template>

<script>
import JQuery from "jquery";
let $ = JQuery;

export default {
  props: ["search"],
  mounted() {
    const target = this.search;

    $("#search-input").on("keyup", function() {
      var value = $(this)
        .val()
        .toLowerCase();
      $(target).filter(function() {
        $(this).toggle(
          $(this)
            .text()
            .toLowerCase()
            .indexOf(value) > -1
        );
      });
    });
  }
};
</script>

<style scoped>
.margin {
  margin-bottom: 5%;
}

.searchbar {
  margin-bottom: auto;
  margin-top: auto;
  background-color: white;
  border-radius: 20px;
  padding: 15px;
  width: 40%;
  border: 2px solid gray;
  min-width: 200px;
}

.search_input {
  color: gray;
  width: 100%;
  border: 0;
  outline: 0;
  background: none;
  padding: 0 15px;
  caret-color: gray;
}
</style>

