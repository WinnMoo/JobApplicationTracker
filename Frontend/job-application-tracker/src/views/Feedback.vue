<template>
  <div class="Feedback" style="text-align:center;">
    <v-container fluid>
      <v-alert v-model="popup" dismissible type="success">{{
        popupText
      }}</v-alert>
      <v-alert v-model="errorPopup" dismissible type="error">{{
        errorMessage
      }}</v-alert>
      <v-row align="center" justify="center">
        <v-col cols="12" sm="6" md="8">
          <v-card style="padding-bottom: 10px;">
            <v-toolbar color="primary" dark flat>
              <v-spacer></v-spacer>
              <v-toolbar-title><h2>Feedback</h2></v-toolbar-title>
              <v-spacer></v-spacer>
            </v-toolbar>
            <v-form>
              <v-row align="center" justify="center">
                <v-col cols="12" sm="6" md="8">
                  <v-row align="center" justify="center">
                    <v-chip-group v-model="category" mandatory>
                      <v-chip
                        v-for="category in categories"
                        v-bind:key="category.category"
                        filter
                        outlined
                        active-class="primary--text text--accent-4"
                      >
                        {{ category.category }}
                      </v-chip>
                    </v-chip-group>
                  </v-row>

                  <v-textarea
                    outlined
                    hint="Feel free to leave any suggestions or feedback about this application"
                    placeholder="I love Jobtaine!"
                    v-model="feedback"
                    width="900"
                    height="250"
                    persistent-hint
                  ></v-textarea>
                  <v-rating
                    v-model="rating"
                    v-if="this.category == 3"
                  ></v-rating>
                  <v-btn color="primary" @click="submit">Submit</v-btn>
                </v-col>
              </v-row>
            </v-form>
          </v-card>
        </v-col>
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
      feedback: null,
      category: 0,
      categories: [
        { category: "Bug" },
        { category: "New Feature" },
        { category: "Suggestion" },
        { category: "Feedback" }
      ],
      errorMessage: null,
      errorPopup: false,
      popupText: null,
      popup: false
    };
  },
  methods: {
    submit: function() {
      if (this.category == 3 && this.feedback == 0) {
        this.errorPopup = true;
        this.errorMessage = "Please select a rating";
      } else if (this.feedback == null) {
        this.errorPopup = true;
        this.errorMessage = "Please leave some feedback";
      } else {
        axios({
          method: "POST",
          url: `${apiURL}/contact/feedback`,
          data: {
            Category: this.category,
            Rating: this.$data.rating,
            Feedback: this.$data.feedback
          },
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Credentials": true
          }
        })
          .then(response => {
            this.popup = response.data;
            this.popupText = "Your feedback has been received. Thank you!";
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
