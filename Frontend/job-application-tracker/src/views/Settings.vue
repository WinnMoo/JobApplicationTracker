<template>
  <div class="Settings" style="text-align:center;" fill-height fluid>
    <v-container>
      <v-alert v-model="popup" dismissible type="success">{{ popupText }}</v-alert>
      <v-alert v-model="errorPopup" dismissible type="error"> {{ errorMessage}} </v-alert>
      <v-row align="center" justify="center">
        <v-col cols="12">
          <v-card class="elevation-12">
            <v-toolbar color="primary" dark flat>
              <v-toolbar-title>Settings</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              <v-row>
                <v-col>
                  <v-switch v-model="showCardView" :label="`Switch 1: ${showCardView.toString()}`"></v-switch>
                </v-col>
              </v-row>
              <v-row>
                <v-col>
                  <v-switch readonly v-model="darkTheme" :label="'Dark Mode'"></v-switch>
                </v-col>
              </v-row>
              <v-row>
                <v-col sm="2" md="2" clss="text-left">
                  <v-btn dark color="error" @click="ShowDeleteDialog">Delete Account</v-btn>
                  <DeleteAccountDialog v-bind:deleteDialog="this.showDeleteDialog"
                   v:on
        @DeleteAccount="DeleteAccount"
                  ></DeleteAccountDialog>
                </v-col>
              </v-row>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" @click="SaveChanges()">Save Changes</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
import axios from "axios";
import { apiURL } from "@/const.js";
import DeleteAccountDialog from "@/components/DeleteAccountDialog.vue";
export default {
  components: {
    DeleteAccountDialog
  },
  name: "settings",
  data() {
    return {
      errorPopup: false,
      errorMessage: null,
      popup: false,
      popupText: null,
      showDeleteDialog: false,
      showCardView: true,
      darkTheme: false
    };
  },
  created() {
    this.showCardView = localStorage.getItem("showCardView");
    if (this.showCardView === null) {
      this.showCardView = true;
      localStorage.setItem("showCardView", this.showCardView);
    }
    this.darkTheme = localStorage.getItem("darkTheme");
    if (this.darkTheme === null) {
      this.darkTheme = false;
      localStorage.setItem("darkTheme", this.darkTheme);
    }
  },
  methods: {
    SaveChanges: function() {
      localStorage.setItem("showCardView", this.showCardView);
      localStorage.setItem("darkTheme", this.darkTheme);
      this.popup = true;
      this.popupText = "Your settings have been saved.";
    },
    ShowDeleteDialog: function() {
      this.showDeleteDialog = true;
    },
    DeleteAccount: function(emailAddress, password) {
      axios({
        method: "POST",
        url: `${apiURL}/account/` + "delete",
        data: {
          EmailAddress: emailAddress,
          Password: password
        },
        headers: {
          "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Credentials": true
        }
      })
        .then(response => {
          this.popup = response;
          this.popupText =
            "Account successfully deleted. You will be redirected to the homepage.";
          setTimeout(() => {
            this.$router.push("/");
          }, 5000);
        })
        .catch(e => {
          this.errorPopup = true;
          this.errorMessage = e.response.data;
        });
    }
  }
};
</script>