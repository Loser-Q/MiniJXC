<template>
  <div>
    <el-dialog v-model="state.showDialog" destroy-on-close :title="state.form.id>0?'编辑付款单':'新增付款单'" draggable :close-on-click-modal="false" width="550px">
      <el-form ref="formRef" :model="state.form" label-width="80px">
        <el-row :gutter="20">
          <el-col :xs="24" :sm="12"><el-form-item label="供应商" prop="supplierId" :rules="[{required:true,message:'请选择供应商'}]"><el-input-number v-model="state.form.supplierId" :min="1" style="width:100%" /></el-form-item></el-col>
          <el-col :xs="24" :sm="12"><el-form-item label="日期"><el-date-picker v-model="state.form.billDate" type="date" value-format="YYYY-MM-DD" style="width:100%" /></el-form-item></el-col>
          <el-col :xs="24" :sm="12"><el-form-item label="金额" prop="amount" :rules="[{required:true,message:'请输入金额'}]"><el-input-number v-model="state.form.amount" :min="0.01" :precision="2" style="width:100%" /></el-form-item></el-col>
          <el-col :xs="24" :sm="12"><el-form-item label="结算账户" prop="accountId" :rules="[{required:true,message:'请选择账户'}]"><el-input-number v-model="state.form.accountId" :min="1" style="width:100%" /></el-form-item></el-col>
          <el-col :xs="24" :sm="12"><el-form-item label="关联购货单"><el-input-number v-model="state.form.purchaseId" :min="1" style="width:100%" /></el-form-item></el-col>
          <el-col :xs="24" :sm="12"><el-form-item label="结算方式"><el-input v-model="state.form.settlementMethod" /></el-form-item></el-col>
          <el-col :xs="24"><el-form-item label="备注"><el-input v-model="state.form.remark" type="textarea" /></el-form-item></el-col>
        </el-row>
      </el-form>
      <template #footer><el-button @click="state.showDialog=false">取消</el-button><el-button type="primary" @click="onSure" :loading="state.sureLoading">确定</el-button></template>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup name="business/payment/form">
import { PaymentEntity } from '/@/api/admin/data-contracts'
import { PaymentApi } from '/@/api/admin/Payment'
import eventBus from '/@/utils/mitt'
import { ElMessage } from 'element-plus'

const formRef = useTemplateRef('formRef')
const state = reactive({ showDialog: false, sureLoading: false, form: {} as any })
const open = async (row: any = {}) => {
  if (row.id > 0) { const res = await new PaymentApi().get({ id: row.id }); if (res?.data) state.form = res.data as any }
  else { state.form = { ...row } }
  state.showDialog = true
}
const onSure = () => {
  formRef.value!.validate(async (valid: boolean) => { if (!valid) return; state.sureLoading = true
    let res: any
    if (state.form.id > 0) res = await new PaymentApi().update(state.form).catch(() => { state.sureLoading = false })
    else res = await new PaymentApi().add(state.form).catch(() => { state.sureLoading = false })
    state.sureLoading = false
    if (res?.success) { ElMessage.success('成功'); eventBus.emit('refreshPayment'); state.showDialog = false }
  })
}
defineExpose({ open })
</script>
