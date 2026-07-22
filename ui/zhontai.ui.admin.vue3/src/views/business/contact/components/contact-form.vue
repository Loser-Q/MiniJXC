<template>
  <div>
    <el-dialog v-model="state.showDialog" destroy-on-close :title="state.form.id > 0 ? '编辑往来单位' : '新增往来单位'" draggable
      :close-on-click-modal="false" width="600px">
      <el-form ref="formRef" :model="state.form" label-width="80px">
        <el-row :gutter="20">
          <el-col :span="24">
            <el-form-item label="类型" prop="type" :rules="[{ required: true, message: '请选择类型' }]">
              <el-radio-group v-model="state.form.type">
                <el-radio value="Customer">客户</el-radio>
                <el-radio value="Supplier">供应商</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="编号" prop="code" :rules="[{ required: true, message: '请输入编号' }]">
              <el-input v-model="state.form.code" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="名称" prop="name" :rules="[{ required: true, message: '请输入名称' }]">
              <el-input v-model="state.form.name" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="联系人"><el-input v-model="state.form.contactPerson" /></el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="电话"><el-input v-model="state.form.phone" /></el-form-item>
          </el-col>
          <el-col :xs="24">
            <el-form-item label="地址"><el-input v-model="state.form.address" /></el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" v-if="state.form.type === 'Supplier'">
            <el-form-item label="期初应付款"><el-input-number v-model="state.form.initPayable" :precision="2" :min="0" style="width:100%" /></el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" v-if="state.form.type === 'Customer'">
            <el-form-item label="期初应收款"><el-input-number v-model="state.form.initReceivable" :precision="2" :min="0" style="width:100%" /></el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="启用"><el-switch v-model="state.form.isEnabled" /></el-form-item>
          </el-col>
          <el-col :xs="24">
            <el-form-item label="备注"><el-input v-model="state.form.remark" type="textarea" /></el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <el-button @click="state.showDialog = false">取消</el-button>
        <el-button type="primary" @click="onSure" :loading="state.sureLoading">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup name="business/contact/form">
import { ContactEntity } from '/@/api/admin/data-contracts'
import { ContactApi } from '/@/api/admin/Contact'
import eventBus from '/@/utils/mitt'
import { FormInstance, ElMessage } from 'element-plus'

const formRef = useTemplateRef<FormInstance>('formRef')
const state = reactive({ showDialog: false, sureLoading: false, form: {} as ContactEntity })

const open = async (row: any = {}) => {
  if (row.id > 0) { const res = await new ContactApi().get({ id: row.id }); if (res?.data) state.form = res.data as any }
  else { state.form = { ...row } as any }
  state.showDialog = true
}

const onSure = () => {
  formRef.value!.validate(async (valid: boolean) => {
    if (!valid) return; state.sureLoading = true
    let res: any
    if ((state.form as any).id > 0) res = await new ContactApi().update(state.form as any).catch(() => { state.sureLoading = false })
    else res = await new ContactApi().add(state.form as any).catch(() => { state.sureLoading = false })
    state.sureLoading = false
    if (res?.success) { ElMessage.success('成功'); eventBus.emit('refreshContact'); state.showDialog = false }
  })
}
defineExpose({ open })
</script>
