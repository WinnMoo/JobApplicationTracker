<template>
  <div class="Settings" style="text-align:center;" fill-height fluid>
    <v-container>
      <v-alert v-model="popup" dismissible type="success">{{ popupText }}</v-alert>
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
export default {
  name: "settings",
  data() {
    return {
      popup: false,
      popupText: null,
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
    this.darkTheme = localStorage.getItem("showCardView");
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
    }
  }
};
</script>