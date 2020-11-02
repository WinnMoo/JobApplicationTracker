<template>
  <div>
    <v-btn
      color="red lighten-2"
      dark
      @click="privacyPolicyDialog = true"
      :style="{ left: '50%', transform: 'translateX(-50%)' }"
      >Privacy Policy</v-btn
    >
    <v-dialog v-model="privacyPolicyDialog" width="500">
      <v-card width="500">
        <v-card-title class="headline blue lighten-2" primary-title
          >Privacy Policy</v-card-title
        >

        <v-card-text
          >Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
          minim veniam, quis nostrud exercitation ullamco laboris nisi ut
          aliquip ex ea commodo consequat. Duis aute irure dolor in
          reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla
          pariatur. Excepteur sint occaecat cupidatat non proident, sunt in
          culpa qui officia deserunt mollit anim id est laborum.</v-card-text
        >

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" text @click="privacyPolicyDialog = false"
            >I accept</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>
<script>
export default {
  name: "PrivacyPolicyDialog",
  props: {},
  data() {
    return {
      privacyPolicyDialog: false
    };
  }
};
</script>
