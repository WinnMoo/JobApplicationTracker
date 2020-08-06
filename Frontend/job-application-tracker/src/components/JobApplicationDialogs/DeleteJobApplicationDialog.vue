<template>
  <v-dialog width="500">
    <v-card>
      <v-card-title>Are you sure you want to delete this?</v-card-title>
      <v-card-text>Deleted applications cannot be restored.</v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          color="primary"
          text
          @click="deleteJobApplication(indexToDelete)"
        >Yes</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "delete-job-application-dialog",
  props: {
  },
  data() {
    return {};
  },
  methods: {
    deleteJobApplication(){
      this.$emit('deleteJobApplication')
    }
  }
};
</script>