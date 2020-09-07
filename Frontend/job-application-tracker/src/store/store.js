import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from "vuex-persistedstate";

Vue.use(Vuex)

export default new Vuex.Store({
  plugins: [createPersistedState({
    storage: window.localStorage,
  })],

  state: {
    isLoggedIn: false
  },

  mutations: {
    change(state, isLoggedIn) {
      state.isLoggedIn = isLoggedIn
    }
  },

  getters: {
    isLoggedIn: state => state.isLoggedIn
  },

  actions: {
    logOut(context) {
      context.commit('change', false);
    },

    logIn(context) {
      context.commit('change', true);
    }
  }
})