<template>
  <div class="Settings" style="text-align:center;" fill-height fluid>
    <v-container>
      <v-row align="center"
      justify="center">
        <v-col cols="12">
          <v-switch v-model="showCardView" :label="`Switch 1: ${showCardView.toString()}`"></v-switch>
        </v-col>
      </v-row>
      <v-row align="center"
      justify="center">
        <v-col cols="12"> 
          <v-switch readonly v-model="darkTheme" :label="'Dark Mode'"></v-switch>
        </v-col>
      </v-row>
      <v-btn color="primary">Save Changes</v-btn>
    </v-container>
  </div>
</template>

<script>
export default {
  name: "settings",
  data() {
    return {
      showCardView: true,
      darkTheme: false
    };
  }
};
</script>