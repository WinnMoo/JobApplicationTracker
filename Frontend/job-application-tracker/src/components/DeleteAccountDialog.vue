<template>
  <div>
    <v-dialog :value="this.showDialog" persistent width="500">
    <v-card>
      <v-card-title>Are you sure you want to delete your account?</v-card-title>
      <v-card-text>Deleted accounts cannot be restored.</v-card-text>
      <v-card-text v-if="this.showDeletionForm">
        <v-text-field label="Email Address" prepend-inner-icon="mdi-account">
        </v-text-field>
        <v-text-field label="Password" prepend-inner-icon="mdi-lock">
        </v-text-field>
      </v-card-text>
      <v-card-actions v-if="!this.showDeletionForm">
        <v-spacer></v-spacer>
        <v-btn color="error" text @click="ShowAccountDeletionForm(true)">Yes</v-btn>
        <v-btn color="primary" text @click="ShowAccountDeletionForm(false)">No</v-btn>
      </v-card-actions>
      <v-card-actions v-if="this.showDeletionForm">
        <v-spacer></v-spacer>
        <v-btn color="error" text @click="DeleteAccount"> Delete Account </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  </div>
</template>

<script>
export default {
  name: "DeleteAccountDialog",
  props: {
    deleteDialog: Boolean
  },
  data() {
    return {
      showDialog: this.deleteDialog,
      showDeletionForm: false,
      emailAddress: null,
      password: null
    };
  },
  watch: {
    deleteDialog: {
      handler(oldVal, newVal) {
        console.log(oldVal + newVal);
        this.showDialog = oldVal;
      }
    }
  },
  methods: {
    ShowAccountDeletionForm: function (deletionCondition) {
      if(deletionCondition){
        this.showDeletionForm = deletionCondition;
      } else {
        this.showDialog = deletionCondition;
      }
    },
    DeleteAccount: function () {
      this.$emit("DeleteAccount", this.emailAddress, this.password);
      this.showDialog = false;
    }
  }
}
</script>