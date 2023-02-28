<template>
    <div id="themeDeleteModal" class="modal">
        <div class="modal-content">
            <h4>{{ $t('message.warning') }}</h4>
            <p>{{ $t('message.delete') }}</p>
        </div>
        <div class="modal-footer">
            <a href="#!" class="modal-close waves-effect waves-green btn-flat">
                {{ $t('button.cancel') }}
            </a>
            <button @click="removeTheme" class="modal-close waves-effect waves-green btn-flat">
                {{ $t('button.confirm') }}
            </button>
        </div>
    </div>
</template>


<script>
import { mapActions } from 'vuex'

export default {
    name: "ThemeDeleteModal",
    methods: {
        ...mapActions([
            'deleteTheme'
        ]),
        open(id) {
            this.$store.commit('setTheme', {id})
            $('#themeDeleteModal').modal('open')
        },
        removeTheme() {
            let id = this.$store.getters['theme'].id
            this.deleteTheme({id})
            .then(() => {
                $('#deleteThemeModal').modal('close')
            })
        }
    },
    mounted() {
        $('.modal').modal()
    }
}
</script>
