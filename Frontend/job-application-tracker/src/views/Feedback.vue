<template>
  <div class="Feedback" style="text-align:center;">
    <h1>Feedback</h1>
    <v-container fill-height fluid>
      <v-row align="center" justify="center">
        <v-form>
          <v-textarea
            outlined
            hint="Feel free to leave any suggestions or feedback about this application"
            placeholder="I love Jobtaine!"
            v-model="feedback"
            width="800"
            persistent-hint
          ></v-textarea>
          <v-rating v-model="rating"></v-rating>
          <v-btn color="primary" @click="submit">Submit</v-btn>
        </v-form>
      </v-row>
    </v-container>
  </div>
</template>

<script>
import axios from "axios";
import { apiURL } from "@/const.js";
export default {
  name: "Feedback",
  data() {
    return {
      rating: 0,
      feedback: null
    };
  },
  methods: {
    submit: function() {
      if (this.rating == 0) {
        alert("Please select a rating");
      } else if (this.feeback == null) {
        alert("Please leave some feedback");
      }else {
        axios({
          method: "POST",
          url: `${apiURL}/feedback/`,
          data: {
            rating: this.$data.rating,
            feedback: this.$data.feedback
          },
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Credentials": true
          }
        })
          .then(response => {
            this.popup = response;
            this.popupText = "Password has been reset.";
          })
          .catch(e => {
            this.formErrorMessage = e.response.data;
          })
          .finally(() => {
            this.loading = false;
          });
      }
    }
  }
};
</script>