<template>
  <v-dialog :value="this.showDialog" persistent width="500">
    <v-card>
      <v-card-title>Are you sure you want to delete this?</v-card-title>
      <v-card-text>Deleted applications cannot be restored.</v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" text @click="deleteJobApplication(true)"
          >Yes</v-btn
        >
        <v-btn color="primary" text @click="deleteJobApplication(false)"
          >No</v-btn
        >
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "DeleteJobApplicationDialog",
  props: {
    deleteDialog: Boolean,
    idToDelete: String
  },
  watch: {
    deleteDialog: {
      handler(oldVal, newVal) {
        this.showDialog = oldVal;
        newVal;
      }
    }
  },
  data() {
    return {
      showDialog: this.deleteDialog
    };
  },
  methods: {
    deleteJobApplication: function(deleteCondition) {
      this.$emit("deleteJobApplication", this.idToDelete, deleteCondition);
      this.showDialog = false;
    }
  }
};
</script>
