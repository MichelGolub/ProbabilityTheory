<template>
    <div id="exerciseDeleteModal" class="modal">
        <div class="modal-content">
            <h4>{{ $t('message.warning') }}</h4>
            <p>{{ $t('message.delete') }}</p>
        </div>
        <div class="modal-footer">
            <a href="#!" class="modal-close waves-effect waves-green btn-flat">
                {{ $t('button.cancel') }}
            </a>
            <button @click="removeExercise" class="modal-close waves-effect waves-green btn-flat">
                {{ $t('button.confirm') }}
            </button>
        </div>
    </div>
</template>


<script>
import { mapActions } from 'vuex'

export default {
    name: "ExerciseDeleteModal",
    methods: {
        ...mapActions([
            'deleteExercise'
        ]),
        open(id) {
            this.$store.commit('setExerciseIdToDelete', id)
            $('#exerciseDeleteModal').modal('open')
        },
        removeExercise() {
            let id = this.$store.getters['exerciseIdToDelete']
            this.deleteExercise({id})
            .then(() => {
                $('#exerciseDeleteModal').modal('close')
            })
        }
    },
    mounted() {
        $('.modal').modal()
    }
}
</script>