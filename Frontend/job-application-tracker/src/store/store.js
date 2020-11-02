import Vue from "vue";
import Vuex from "vuex";
import createPersistedState from "vuex-persistedstate";

Vue.use(Vuex);

export default new Vuex.Store({
  plugins: [
    createPersistedState({
      storage: window.localStorage
    })
  ],

  state: {
    isLoggedIn: false,
    emailAddress: null,
    firstName: null
  },

  mutations: {
    changeLoginStatus(state, isLoggedIn) {
      state.isLoggedIn = isLoggedIn;
    },

    changeEmailAddress(state, emailAddress) {
      state.emailAddress = emailAddress;
    },

    changeFirstName(state, firstName) {
      state.firstName = firstName;
    }
  },

  getters: {
    isLoggedIn: state => state.isLoggedIn,
    emailAddress: state => state.emailAddress,
    firstName: state => state.firstName
  },

  actions: {
    logOut(context) {
      context.commit("changeLoginStatus", false);
    },

    logIn(context) {
      context.commit("changeLoginStatus", true);
    },

    setEmailAddress(context, emailAddress) {
      context.commit("changeEmailAddress", emailAddress);
    },

    removeEmailAddress(context) {
      context.commit("changeEmailAddress", null);
    },

    setFirstName(context, firstName) {
      context.commit("changeFirstName", firstName);
    },
    removeFirstName(context) {
      context.commit("changeFirstName", null);
    }
  }
});
